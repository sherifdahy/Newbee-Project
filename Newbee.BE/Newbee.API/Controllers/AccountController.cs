using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Users;
using Newbee.BLL.Extensions;
namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult>GetProfileInfo(int userId)
    {
        var result = await  _userService.GetProfileAsync(userId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("info")]
    public async Task<IActionResult> Update([FromForm] UpdateProfileRequest request)
    {
        int userId = int.Parse(User.GetUserId()!);
        var result = await _userService.UpdateProfileAsync(userId, request);
        return NoContent();
    }
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(int.Parse(User.GetUserId()!), request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
