using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActiveCodesController(IActiveCodesService activeCodeService) : ControllerBase
{
    private readonly IActiveCodesService activeCodeService = activeCodeService;

    [HttpGet]
    [HasPermission(Permissions.GetActiveCodes)]
    public async Task<IActionResult> GetAll()
    {
        var result = await activeCodeService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetActiveCodes)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await activeCodeService.GetById(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateActiveCodes)]
    public async Task<IActionResult> Create(ActiveCodeRequest request)
    {
        var result = await activeCodeService.CreateAsync( request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateActiveCodes)]
    public async Task<IActionResult> Update(int id, ActiveCodeRequest request)
    {
        var result = await activeCodeService.UpdateAsync(id, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{id}")]
    [HasPermission(Permissions.ToggleStatusActiveCodes)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await activeCodeService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
