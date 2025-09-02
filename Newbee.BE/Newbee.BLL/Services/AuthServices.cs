using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newbee.BLL.DTO.Mail;
using Newbee.BLL.Authentication;
using System.Security.Cryptography;
using Newbee.BLL.DTO.Auth.Responses;
namespace Newbee.BLL.Services;

public class AuthServices(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager,
     ILogger<AuthServices> logger,
     IEmailSender emailSender,
     IJwtProvider jwtProvider,
     EmailBuilder _builder) : IAuthServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<AuthServices> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IJwtProvider jwtProvider = jwtProvider;
    private readonly EmailBuilder _builder = _builder;
    private readonly int _otpExpiryMinutes = 5;
    private readonly int _resetRefreshTokenExpiryDays = 14;

    public async Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        //check user?
        var user = await _userManager.FindByEmailAsync(request.Email);
        if(user is null)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidCredentials);
        // check password?
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
          if(!isValidPassword)
            return Result.Failure<AuthResponse?>(UserErrors.InvalidCredentials);
        //check email confirmed?
        if (!user.EmailConfirmed)
            return Result.Failure<AuthResponse?>(UserErrors.EmailNotConfirmed);
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        //generate token
        if (result.Succeeded)
        {
            var (token, expiresIn) = jwtProvider.GenerateToken(user);
            var refreshToken =GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_resetRefreshTokenExpiryDays);
           user.RefreshTokens.Add(new RefreshToken
           {
               Token = refreshToken,
               ExpiresOn = refreshTokenExpiry
           });
            await _userManager.UpdateAsync(user);
            var response = new AuthResponse(user.Id, user.Email, user.FirstName,user.LastName, token, expiresIn,refreshToken,refreshTokenExpiry);
        // return new auth response

            return Result.Success(response);

        }
        return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

    }

    //public async Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    //{

    //    var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

    //    if (emailIsExists)
    //        return Result.Failure(UserErrors.DuplicatedEmail);
    //    var company = new Company
    //    {
    //        Name = request.CompanyName,
    //        TaxRegistrationNumber = request.TaxNumber,
    //    }; _unitOfWork.Companies.Add(company);

    //    await _unitOfWork.SaveAsync();
    //    var user = request.Adapt<ApplicationUser>();
    //    user.UserName = request.Email;
    //    user.CompanyId = company.Id;
    //    var result = await _userManager.CreateAsync(user, request.Password);
    //    // var roleResult = await _userManager.AddToRoleAsync(user, "merchant");

    //    if (result.Succeeded)
    //    {

    //        await SendOtpAsync(user);
    //        company = request.Adapt<Company>();
    //        await _unitOfWork.Companies.AddAsync(company);
    //        await _unitOfWork.SaveAsync();
    //        return Result.Success(user.Id);
    //    }

    //    var error = result.Errors.First();

    //    return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    //}
    public async Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        
        bool emailExists = await _userManager.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        
        var company = new Company
        {
            Name = request.CompanyName,
            TaxRegistrationNumber = request.TaxNumber,
        };

        await _unitOfWork.SaveAsync();
        _unitOfWork.Companies.Add(company);
        await _unitOfWork.SaveAsync();

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;
        user.CompanyId = company.Id;

        var createUserResult = await _userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            await SendOtpAsync(user);
           
            await _unitOfWork.SaveAsync();
            return Result.Success(user.Id);
            var error = createUserResult.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        
        await SendOtpAsync(user);

        

        return Result.Success(); 
    }


    public async Task<Result> ConfirmEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
    {
        var EmailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (!EmailIsExist)
            return Result.Failure(UserErrors.EmailNotFound);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (request.Code == null || request.Code.Length != 6 || !request.Code.All(char.IsDigit))
            return Result.Failure(UserErrors.InvalidCode);
        var otp = await _unitOfWork.OTPs.FindAsync(x => x.ApplicationUserId == user!.Id && x.Code == request.Code);
        if (otp == null)
            return Result.Failure(UserErrors.InvalidCode);
        if (otp.ExpiryTime < DateTime.UtcNow)
            return Result.Failure(UserErrors.InvalidCode);
        user.EmailConfirmed = true;
        _unitOfWork.OTPs.Delete(otp);
        await _userManager.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
        return Result.Success();

    }
    private async Task SendOtpAsync(ApplicationUser user)
    {
        var otpCode = new Random().Next(100000, 999999).ToString();

        var existingOtps =  _unitOfWork.OTPs.FindAll(x => x.ApplicationUserId == user.Id)
        .ToList();

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
            }
        );

        await _emailSender.SendEmailAsync(user.Email!, "✅ NewBee: Email Verification OTP", emailBody);

        _logger.LogInformation("OTP sent to user {UserId}: {OtpCode}", user.Id, otpCode);

    }

   
    public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = jwtProvider.ValidateToken(token);
        if (userId == null)
            Result.Failure<AuthResponse?>(UserErrors.InvalidJwtToken);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            Result.Failure<AuthResponse?>(UserErrors.InvalidJwtToken);
        var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken&& rt.IsActive);
        if (userRefreshToken == null)
            Result.Failure<AuthResponse?>(UserErrors.InvalidRefreshToken);
        userRefreshToken.RevokedOn = DateTime.UtcNow;
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(_resetRefreshTokenExpiryDays);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiry
        });
        await _userManager.UpdateAsync(user);
        var (newJwtToken, expiresIn) = jwtProvider.GenerateToken(user);
        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newJwtToken, expiresIn, newRefreshToken, refreshTokenExpiry);
        return  response;
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

}
