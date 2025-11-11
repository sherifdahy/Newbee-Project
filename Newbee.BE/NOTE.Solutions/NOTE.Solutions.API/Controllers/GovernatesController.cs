using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.BLL.Contracts.Governate.Requests;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GovernatesController(IGovernateService governateService) : ControllerBase
{
    private readonly IGovernateService _governateService = governateService;

    [HttpGet]
    [HasPermission(Permissions.GetGovernorates)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _governateService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("get-related")]
    [HasPermission(Permissions.GetGovernorates)]
    public async Task<IActionResult> GetRelated([FromQuery] int countryId)
    {
        var result = await _governateService.GetRelatedAsync(countryId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{governateId:int}")]
    [HasPermission(Permissions.GetGovernorates)]
    public async Task<IActionResult> GetById(int governateId)
    {
        var result = await _governateService.GetByIdAsync(governateId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateGovernorates)]
    public async Task<IActionResult> Create([FromBody] GovernateRequest request)
    {
        var result = await _governateService.CreateAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { governateId = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{governateId:int}")]
    [HasPermission(Permissions.UpdateGovernorates)]
    public async Task<IActionResult> Update(int governateId, [FromBody] GovernateRequest request)
    {
        var result = await _governateService.UpdateAsync(governateId, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{governateId:int}")]
    [HasPermission(Permissions.ToggleStatusGovernorates)]
    public async Task<IActionResult> Delete(int governateId)
    {
        var result = await _governateService.DeleteAsync(governateId);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
