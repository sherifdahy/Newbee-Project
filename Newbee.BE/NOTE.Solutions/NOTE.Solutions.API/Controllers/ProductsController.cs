using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.Product.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("~/api/branches/{branchId:int}/[controller]")]
    public async Task<IActionResult> GetAll(int branchId,CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync(branchId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetById(int productId,CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(productId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("~/api/branches/{branchId:int}/[controller]")]
    public async Task<IActionResult> Create(int branchId, [FromBody] ProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _productService.CreateAsync(branchId, request,cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { productId = result.Value.Id }, result.Value) : result.ToProblem();
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
