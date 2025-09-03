using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Product.Requests;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IProductService productService) :BaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _productService.GetAllAsync(CompanyId,cancellationToken);

        return result.IsSuccess ? Ok(result.Value.Adapt<IEnumerable<ProductResponse>>()) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
    {
        var result = await _productService.GetByIdAsync(id,cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<ProductResponse>()) : result.ToProblem();
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductRequest request,CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(id, request.Adapt<Product>(),cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(id,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateAsync(CompanyId,request.Adapt<Product>(),cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetAll), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }
}
