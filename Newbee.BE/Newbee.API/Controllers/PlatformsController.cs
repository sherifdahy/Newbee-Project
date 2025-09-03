using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Platform.Requests;


namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class PlatformsController(IPlatformService platformService) : BaseController
{
    private readonly IPlatformService _platformService = platformService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _platformService.GetAllAsync( cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _platformService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PlatformRequest request, CancellationToken cancellationToken)
    {
        var result = await _platformService.CreateAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Create), result.Value.Id, result) : result.ToProblem();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _platformService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PlatformRequest request, CancellationToken cancellationToken)
    {
        var result = await _platformService.UpdateAsync(id, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
