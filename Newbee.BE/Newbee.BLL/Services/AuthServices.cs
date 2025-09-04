using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newbee.BLL.DTO.Mail;
using Newbee.BLL.Authentication;
using System.Security.Cryptography;
using Newbee.BLL.DTO.Auth.Requests;
using Newbee.BLL.DTO.Auth.Responses;
using Newbee.Entities.Models;

namespace Newbee.BLL.Services;

public class AuthServices(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager, ILogger<AuthServices> logger,
    IEmailSender emailSender, IJwtProvider jwtProvider, EmailBuilder builder) : IAuthServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<AuthServices> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly EmailBuilder _builder = builder;
    private readonly int _otpExpiryMinutes = 5;
    private readonly int _resetRefreshTokenExpiryDays = 14;

    public async Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidCredentials);

        // Check password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidCredentials);

        // Check email confirmed
        if (!user.EmailConfirmed)
            return Result.Failure<AuthResponse?>(UserErrors.EmailNotConfirmed);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        // Generate token
        if (result.Succeeded)
        {
            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_resetRefreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiry
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName,
                token, expiresIn, refreshToken, refreshTokenExpiry);

            return Result.Success<AuthResponse?>(response);
        }

        return Result.Failure<AuthResponse?>(UserErrors.InvalidCredentials);
    }

    public async Task<Result> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var applicationUser = request.Adapt<ApplicationUser>();

        var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var identityResult = await _userManager.CreateAsync(applicationUser, request.Password);

            if (!identityResult.Succeeded)
            {
                var error = identityResult.Errors.First();
                return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }


            await SendOtpAsync(applicationUser);

            return Result.Success();
        });

        return result;
    }
    public async Task<Result> RegisterCustomerAsync(RegisterCustomerRequest request,Guid apiKey,CancellationToken cancellationToken = default)
    {
        var currentCompany = await _unitOfWork.Companies.FindAsync(x => x.ApiKey == apiKey);

        if (currentCompany is null)
            return Result.Failure(CompanyErrors.InvalidId);

        var emailIsExits = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (emailIsExits)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var applicationUser = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            CompanyId = currentCompany.Id,
        };

        var result = await _unitOfWork.ExecuteInTransactionAsync<Result>(async () =>
        {
            var identityResult = await _userManager.CreateAsync(applicationUser, request.Password);
            if (!identityResult.Succeeded)
            {
                var error = identityResult.Errors.First();
                return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }

            var customer = request.Adapt<Customer>();

            customer.ApplicationUserId = applicationUser.Id;

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveAsync();

            await SendOtpAsync(applicationUser);

            return Result.Success();
        });

        return result;
    }


    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        var emailExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (!emailExists)
            return Result.Failure(UserErrors.EmailNotFound);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (request.Code == null || request.Code.Length != 6 || !request.Code.All(char.IsDigit))
            return Result.Failure(UserErrors.InvalidCode);

        var otp = await _unitOfWork.OTPs.FindAsync(x => x.ApplicationUserId == user!.Id && x.Code == request.Code);
        if (otp == null)
            return Result.Failure(UserErrors.InvalidCode);

        if (otp.ExpiryTime < DateTime.UtcNow)
            return Result.Failure(UserErrors.InvalidCode);

        user!.EmailConfirmed = true;
        _unitOfWork.OTPs.Delete(otp);
        await _userManager.UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);
        if (userId == null)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
        if (userRefreshToken == null)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(_resetRefreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiry
        });

        await _userManager.UpdateAsync(user);

        var (newJwtToken, expiresIn) = _jwtProvider.GenerateToken(user);
        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName,
            newJwtToken, expiresIn, newRefreshToken, refreshTokenExpiry);

        return Result.Success<AuthResponse?>(response);
    }

    public async Task<Result> ResendConfirmationEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Success();

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.DuplicatedConfirmation);

        await SendOtpAsync(user);
        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCode);

        var otpRecord = await _unitOfWork.OTPs
            .FindAsync(x => x.ApplicationUserId == user.Id && x.Code == request.Code);

        if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
        {
            if (otpRecord != null)
            {
                _unitOfWork.OTPs.Delete(otpRecord);
                await _unitOfWork.SaveAsync();
            }
            return Result.Failure(UserErrors.InvalidCode);
        }

        _unitOfWork.OTPs.Delete(otpRecord);
        await _unitOfWork.SaveAsync();

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.newPassword);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> SendResetOtpAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Failure(UserErrors.EmailNotFound);

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        await SendPasswordResetOtpAsync(user);
        return Result.Success();
    }

    private async Task<Result> SendPasswordResetOtpAsync(ApplicationUser user)
    {
        try
        {
            var otpCode = GenerateSecureOtpCode();

            var existingOtps = await _unitOfWork.OTPs.FindAllAsync(x => x.ApplicationUserId == user.Id);
            if (existingOtps.Any())
            {
                _unitOfWork.OTPs.DeleteRange(existingOtps);
            }

            var otp = new OTP
            {
                Code = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(_otpExpiryMinutes),
                ApplicationUserId = user.Id,
                User = user
            };

            _unitOfWork.OTPs.Add(otp);
            await _unitOfWork.SaveAsync();

            var fullName = $"{user.FirstName} {user.LastName}";
            var emailBody = _builder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", fullName },
                    { "{{otp_code}}", otpCode },
                    { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
                });

            await _emailSender.SendEmailAsync(user.Email!, "🔐 NewBee: Password Reset OTP", emailBody);
            _logger.LogInformation("Password reset OTP sent to user {UserId}", user.Id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending password reset OTP to user {UserId}", user.Id);
            return Result.Failure(new Error("EmailSendError", "Failed to send OTP email", StatusCodes.Status500InternalServerError));
        }
    }

    private async Task SendOtpAsync(ApplicationUser user)
    {
        var otpCode = GenerateSecureOtpCode();

        var existingOtps = _unitOfWork.OTPs.FindAll(x => x.ApplicationUserId == user.Id).ToList();
        if (existingOtps.Any())
        {
            _unitOfWork.OTPs.DeleteRange(existingOtps);
        }

        var otp = new OTP
        {
            Code = otpCode,
            ExpiryTime = DateTime.UtcNow.AddMinutes(_otpExpiryMinutes),
            ApplicationUserId = user.Id,
            User = user
        };

        await _unitOfWork.OTPs.AddAsync(otp);
        await _unitOfWork.SaveAsync();

        var emailBody = _builder.GenerateEmailBody("EmailConfirmation",
            templateModel: new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                { "{{otp_code}}", otpCode },
                { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
            });

        await _emailSender.SendEmailAsync(user.Email!, "✅ NewBee: Email Verification OTP", emailBody);
        _logger.LogInformation("OTP sent to user {UserId}", user.Id);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private static string GenerateSecureOtpCode()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        var randomNumber = BitConverter.ToUInt32(bytes, 0);
        return (randomNumber % 900000 + 100000).ToString();
    }

  
}