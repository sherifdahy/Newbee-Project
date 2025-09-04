using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.DTO.ProductCategory.Requests;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductCategoriesController(IProductCategoryService productCategoryService) : BaseController
{
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.GetAllAsync(CompanyId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.GetByIdAsync(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCategoryRequest request , CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.CreateAsync(CompanyId, request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Create),result.Value.Id,result.Value) : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody]ProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.UpdateAsync(id,request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
