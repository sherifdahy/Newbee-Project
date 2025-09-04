using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.DTO.Mail;
using Newbee.DAL.Abstractions;
using System.Threading;
using LoginRequest = Newbee.BLL.DTO.Auth.Requests.LoginRequest;
using RegisterCompanyRequest = Newbee.BLL.DTO.Auth.Requests.RegisterCompanyRequest;

namespace Newbee.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthServices authServices) : BaseController
{
    private readonly IAuthServices _authServices = authServices;
    [HttpPost("register-company")]
    public async Task<IActionResult> Register([FromBody] RegisterCompanyRequest request,CancellationToken cancellationToken)
    {
        var result = await _authServices.RegisterCompanyAsync(request,cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromHeader(Name = "X-Api-Key")] Guid apiKey, [FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await _authServices.RegisterCustomerAsync(request,apiKey, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request,CancellationToken cancellationToken)
    {
        var result = await _authServices.GetTokenAsync(request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _authServices.ConfirmEmailAsync(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResult = await _authServices.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
    }



    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _authServices.ResendConfirmationEmailAsync(request.Email,cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {
        var result = await _authServices.SendResetOtpAsync(request.Email);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] BLL.DTO.Auth.Requests.ResetPasswordRequest request)
    {
        var result = await _authServices.ResetPasswordAsync(request);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

}
