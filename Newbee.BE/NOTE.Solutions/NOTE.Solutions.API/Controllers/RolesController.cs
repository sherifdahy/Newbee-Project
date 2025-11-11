using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [HttpGet]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled,CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllAsync(includeDisabled, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetById([FromRoute]int id,CancellationToken cancellationToken = default)
    {
        var result = await _roleService.GetByIdAsync(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost]
    [HasPermission(Permissions.CreateRoles)]
    public async Task<IActionResult> Create([FromBody] RoleRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleService.CreateAsync(request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }


    [HttpPut("{id:int}")]
    //[HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromRoute] int id,[FromBody] RoleRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _roleService.UpdateAsync(id,request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("{id:int}/toggle-status")]
    [HasPermission(Permissions.ToggleStatus)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _roleService.ToggleStatus(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
