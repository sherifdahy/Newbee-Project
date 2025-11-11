using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.POS.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/branches/{branchId:int}/[controller]")]
[ApiController]
public class POSsController(IPointOfSaleService pOSService) : ControllerBase
{
    private readonly IPointOfSaleService _pOSService = pOSService;

    [HttpGet("/api/branches/{branchId}/point-of-sales")]
    public async Task<IActionResult> GetAll(int branchId, CancellationToken cancellationToken)
    {
        var result = await _pOSService.GetAllAsync(branchId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/point-of-sales/{posId}")]
    public async Task<IActionResult> GetById(int branchId, int posId, CancellationToken cancellationToken)
    {
        var result = await _pOSService.GetById(posId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/branches/{branchId}/point-of-sales")]
    public async Task<IActionResult> Create(int branchId, PointOfSaleRequest request, CancellationToken cancellationToken)
    {
        var result = await _pOSService.CreateAsync(branchId, request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpPut("/api/point-of-sales/{posId}")]
    public async Task<IActionResult> Update(int posId, PointOfSaleRequest request, CancellationToken cancellationToken)
    {
        var result = await _pOSService.UpdateAsync(posId,request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("/api/point-of-sales/{posId}")]
    public async Task<IActionResult> Delete(int posId,CancellationToken cancellationToken)
    {
        var result = await _pOSService.DeleteAsync(posId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
