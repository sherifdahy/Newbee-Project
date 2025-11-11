using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.Manager.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagersController(IManagerService managerService) : ControllerBase
{
    private readonly IManagerService _managerService = managerService;

    [HttpPost("/api/companies/{companyId:int}/[controller]")]
    public async Task<IActionResult> Create(int companyId,ManagerRequest request,CancellationToken cancellationToken)
    {
        var result = await _managerService.CreateAsync(companyId,request,cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById),new { managerId = result.Value.Id },result.Value) : result.ToProblem();
    }

    [HttpGet("{managerId:int}")]
    public async Task<IActionResult> GetById(int managerId,CancellationToken cancellationToken = default)
    {
        var result = await _managerService.GetByIdAsync(managerId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/companies/{companyId}/managers")]
    public async Task<IActionResult> GetAll(int companyId, CancellationToken cancellationToken = default)
    {
        var result = await _managerService.GetAllAsync(companyId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _managerService.GetAllAsync(cancellationToken:cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{managerId:int}")]
    public async Task<IActionResult> Delete(int managerId,CancellationToken cancellationToken = default)
    {
        var result = await _managerService.DeleteAsync(managerId,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("/api/companies/{companyId:int}/managers/{managerId:int}")]
    public async Task<IActionResult> Update(int companyId,int managerId,ManagerRequest request,CancellationToken cancellationToken = default)
    {
        var result = await _managerService.UpdateAsync(companyId,managerId,request,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
