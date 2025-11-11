namespace NOTE.Solutions.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _service = service;
    [HttpPost("{branchId}")]
    public async Task<IActionResult> Create([FromBody] CategoryRequest request,[FromRoute]int branchId, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, branchId, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Create), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id ,CancellationToken cancellationToken)
        {
        var result = await _service.GetAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryRequest request, CancellationToken cancellationToken)
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
