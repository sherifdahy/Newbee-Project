using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Contracts.Document.Requests;
using System.Threading.Tasks;

namespace NOTE.Solutions.API.Controllers;

[Route("api/branches/{branchId:int}/[controller]")]
[ApiController]
[Authorize]
public class ReceiptsController(IReceiptService receiptService) : ControllerBase
{
    private readonly IReceiptService _receiptService = receiptService;

    [HttpGet()]
    public async Task<IActionResult> GetAll(int branchId,CancellationToken cancellationToken)
    {
        var result = await _receiptService.GetAllAsync(branchId,cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
    {
        var result = await _receiptService.GetByIdAsync(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("next-number")]
    public async Task<IActionResult> GetNextNumberAsync(int branchId)
    {
        var result = await _receiptService.GetNextNumberAsync(branchId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Create(int branchId,OrderRequest request,CancellationToken cancellationToken)
    {
        var result = await _receiptService.CreateAsync(branchId,request, cancellationToken);

        return result.IsSuccess ? Created() : result.ToProblem();
    }
}
