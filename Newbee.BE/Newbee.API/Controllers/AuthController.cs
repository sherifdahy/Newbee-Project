using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.DTO.Mail;
using System.Threading;

namespace Newbee.API.Controllers
{
    public class AuthController(IAuthServices authServices) : BaseController
    {
        private readonly IAuthServices _authServices = authServices;
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request,CancellationToken cancellationToken)
        {
            var result = await _authServices.RegisterMerchantAsync(request,cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request,CancellationToken cancellationToken)
        {
            var result = await _authServices.GetTokenAsync(request);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] MailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authServices.ConfirmEmailAsync(request, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        //[HttpPost("resend-otp")]
        //public async Task<IActionResult> ResendOtp([FromBody] string email, CancellationToken cancellationToken)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user is null) return NotFound();

        //    await SendOtpAsync(user);
        //    return Ok();
        //}

    }
}
