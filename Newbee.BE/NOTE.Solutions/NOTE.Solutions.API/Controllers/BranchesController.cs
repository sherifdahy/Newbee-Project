using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BranchesController(IBranchService branchService) : ControllerBase
{
    private readonly IBranchService _branchService = branchService;

    [HttpGet("~/api/companies/{companyId:int}/[controller]")]
    [HasPermission(Permissions.GetBranches)]
    public async Task<IActionResult> GetAll(int companyId, CancellationToken cancellationToken)
    {
        var result = await _branchService.GetAllAsync(companyId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
    {
        var result = await _branchService.GetById(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/companies/{companyId}/branches")]
    public async Task<IActionResult> Create([FromRoute]int companyId, [FromBody]BranchRequest request,CancellationToken cancellationToken)
    {
        var result = await _branchService.CreateAsync(companyId, request,cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, BranchRequest request,CancellationToken cancellationToken)
    {
        var result = await _branchService.UpdateAsync(id,request,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _branchService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}