using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.DTO.ProductCategory.Requests;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductCategoriesController(IProductCategoryService productCategoryService) : BaseController
{
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.GetAllAsync(CompanyId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<IEnumerable<ProductCategoryResponse>>()) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.GetByIdAsync(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<ProductCategoryResponse>()) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCategoryRequest request , CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.CreateAsync(CompanyId, request.Adapt<ProductCategory>(), cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Create),result.Value.Id,result.Value.Adapt<ProductCategoryResponse>()) : result.ToProblem();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody]ProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _productCategoryService.UpdateAsync(id,request.Adapt<ProductCategory>(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
