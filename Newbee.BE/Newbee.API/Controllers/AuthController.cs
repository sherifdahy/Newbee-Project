using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
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
            return result.IsSuccess ? Ok(result.value) : result.ToProblem();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request,CancellationToken cancellationToken)
        {
            var result = await _authServices.GetTokenAsync(request);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
