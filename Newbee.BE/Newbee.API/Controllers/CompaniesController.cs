using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Company.Responses;

namespace Newbee.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CompaniesController(ICompanyService companyService) : BaseController
{
    private readonly ICompanyService _companyService = companyService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _companyService.GetAllAsync(cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _companyService.GetByIdAsync(id, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CompanyRequest request, CancellationToken cancellationToken)
    {
        var result = await _companyService.UpdateAsync(id, request, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _companyService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CompanyRequest request, CancellationToken cancellationToken)
    {
        var result = await _companyService.CreateAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value.Adapt<CompanyResponse>())
            : result.ToProblem();
    }
}
