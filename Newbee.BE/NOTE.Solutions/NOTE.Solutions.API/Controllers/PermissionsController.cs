using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PermissionsController : ControllerBase
{
    [HttpGet]
    //[HasPermission(Permissions.GetPermissions)]
    public IActionResult GetAll()
    {
        var permissions = Permissions.GetAllPermissions();
        return Ok(permissions);
    }
}
