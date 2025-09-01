using Microsoft.AspNetCore.Mvc;

namespace Newbee.API.Controllers
{
    public class AuthController(IAuthServices authServices) : BaseController
    {
        private readonly IAuthServices _authServices = authServices;
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authServices.RegisterMerchantAsync(model);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authServices.LoginAsync(model);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
