using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newbee.BLL.Errors;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newbee.BLL.Services.Email;
namespace Newbee.BLL.Services.Auth;

public class AuthServices(IUnitOfWork unitOfWork ,UserManager<ApplicationUser> userManager
    , ILogger<AuthServices> logger,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor,

     EmailBuilder _builder) : IAuthServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<AuthServices> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly EmailBuilder _builder = _builder;
    private readonly int _otpExpiryMinutes = 5;

    public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
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
