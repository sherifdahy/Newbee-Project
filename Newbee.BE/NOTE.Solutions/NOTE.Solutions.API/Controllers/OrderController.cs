using NOTE.Solutions.BLL.Contracts.Document.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController(IOrderService service) : ControllerBase
{
    private readonly IOrderService _service = service;

    [HttpPost("/api/companies/{companyId}/orders")]
    public async Task<IActionResult> Create([FromRoute] int companyId, [FromBody] OrderRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, companyId, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Create), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpGet("/api/companies/{companyId}/orders")]
    public async Task<IActionResult> GetAll(int companyId, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(companyId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _service.GetAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/restore")]
    public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
    {
        var result = await _service.RestoreAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}