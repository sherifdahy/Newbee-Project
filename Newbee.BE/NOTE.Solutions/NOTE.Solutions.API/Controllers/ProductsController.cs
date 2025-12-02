using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.Product.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IProductService productService) : ControllerBase
{
    // crud operations for product done
    private readonly IProductService _productService = productService;

    [HttpGet("~/api/categories/{categoryId:int}/[controller]")]
    public async Task<IActionResult> GetAll(int categoryId, CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync(categoryId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetById(int productId,CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(productId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("~/api/categories/{categoryId:int}/[controller]")]
    public async Task<IActionResult> Create(  [FromRoute] int categoryId, [FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateAsync( categoryId, request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { productId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Update(int productId, [FromBody] ProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(productId, request,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Delete(int productId,CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(productId,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
