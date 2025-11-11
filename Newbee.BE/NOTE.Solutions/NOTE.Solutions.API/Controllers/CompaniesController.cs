using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.Entities.Abstractions.Consts;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    private readonly ICompanyService companyService = companyService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int? userId = null,CancellationToken cancellationToken = default)
    {
        var result = await companyService.GetAllAsync(userId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{companyId:int}")]
    public async Task<IActionResult> GetById(int companyId)
    {
        var result = await companyService.GetByIdAsync(companyId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyRequest request,CancellationToken cancellationToken)
    {
        var result = await companyService.CreateAsync(request,cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById),new {companyId = result.Value.Id},result.Value) : result.ToProblem();
    }

    [HttpPut("{companyId:int}")]
    public async Task<IActionResult> UpdateAsync(int companyId, [FromBody] UpdateCompanyRequest request)
    {
        var result = await companyService.UpdateAsync(companyId, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{companyId:int}")]
    public async Task<IActionResult> DeleteAsync(int companyId)
    {
        var result = await companyService.DeleteAsync(companyId);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
