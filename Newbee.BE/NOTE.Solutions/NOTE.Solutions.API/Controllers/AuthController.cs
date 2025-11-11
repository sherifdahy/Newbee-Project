using NOTE.Solutions.BLL.Contracts.Auth.Requests;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("get-token")]
    public async Task<IActionResult> GetToken(LoginRequest authRequest, CancellationToken cancellationToken)
    {
        var result = await _authService.GetTokenAsync(authRequest, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request ,CancellationToken cancellationToken)
    {
        var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken,cancellationToken);
        return authResult.IsSuccess ? Ok(authResult.Value) : authResult.ToProblem();
    }

    [HttpPost("register-company")]
    public async Task<IActionResult> Register(RegisterCompanyRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterCompanyAsync(request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke(RefreshTokenRequest request,CancellationToken cancellationToken)
    {
        var result = await _authService.RevokeAsync(request.Token, request.RefreshToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

}
