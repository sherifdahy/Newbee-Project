using Api.Bosta.DTOs.Auth.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.DTO.Authentication;
using Newbee.BLL.Services.Auth;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IAuthServices _authService;
    public ValuesController(IAuthServices authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task <IActionResult> Register(RegisterRequest registerRequest,CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(registerRequest,cancellationToken);

        return result.IsSuccess ? Ok(result)  : BadRequest(result.Error);
    }
}
