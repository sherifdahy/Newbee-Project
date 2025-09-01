using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newbee.BLL.Errors;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newbee.BLL.Services.Email;
<<<<<<< HEAD
=======
using Newbee.Entities;
using Newbee.BLL.DTO.Mail;
using Newbee.BLL.Authentication;
using Microsoft.Extensions.Options;
>>>>>>> 2282cb2f709b636a98913456b9f6df9a366c7de9
namespace Newbee.BLL.Services.Auth;

public class AuthServices(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager
    , ILogger<AuthServices> logger,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor,
    IJwtProvider jwtProvider,
     EmailBuilder _builder,
     IOptions<JwtOptions>options) : IAuthServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<AuthServices> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly IJwtProvider jwtProvider = jwtProvider;
    private readonly EmailBuilder _builder = _builder;
    private readonly JwtOptions options = options.Value;
    private readonly int _otpExpiryMinutes = 5;

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
            var response = new AuthResponse(user.Id, user.Email, user.FirstName,user.LastName, token, expiresIn);
        // return new auth response

            return Result.Success(response);

        }
        return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

    }

    public async Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {

        var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();
        user.UserName = request.Email;
       await  _userManager.AddToRoleAsync(user, "merchant");
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {

            await SendOtpAsync(user);
            var company = request.Adapt<Company>();
            _unitOfWork.Companies.Add(company);
            _unitOfWork.Save();
            return Result.Success(user.Id);
        }

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

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
        _unitOfWork.Save();
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

        _unitOfWork.OTPs.Add(otp);
         _unitOfWork.Save();

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


  

  
}
