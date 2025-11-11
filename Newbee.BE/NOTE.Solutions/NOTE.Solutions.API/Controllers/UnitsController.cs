using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.Unit.Requests;
using System.Threading.Tasks;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UnitsController(IUnitService unitService) : ControllerBase
{
    private readonly IUnitService _unitService = unitService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _unitService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
    {
        var result = await _unitService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UnitRequest request,CancellationToken cancellationToken)
    {
        var result = await _unitService.CreateAsync(request,cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById),new {id = result.Value.Id}) : result.ToProblem();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id,UnitRequest request,CancellationToken cancellationToken)
    {
        var result = await _unitService.UpdateAsync(id,request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _unitService.DeleteAsync(id,cancellationToken);
        return result.IsSuccess? NoContent() : result.ToProblem();
    }
}
