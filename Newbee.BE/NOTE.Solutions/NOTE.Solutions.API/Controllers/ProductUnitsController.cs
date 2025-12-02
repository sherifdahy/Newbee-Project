using Microsoft.AspNetCore.Authorization;
using NOTE.Solutions.BLL.Contracts.ProductUnit.Requests;

namespace NOTE.Solutions.API.Controllers;
[Route("api/products/{productId}/[controller]")]
[ApiController]
[Authorize]
public class ProductUnitsController : ControllerBase
{
    // crud operations for product unit done
    private readonly IProductUnitService _productUnitService;

    public ProductUnitsController(IProductUnitService productUnitService)
    {
        _productUnitService = productUnitService;
    }

    [HttpGet("~/api/categories/{categoryId}/productUnits")]
    public async Task<IActionResult> GetAll(int categoryId)
    {
        var result = await _productUnitService.GetAllAsync(categoryId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{productUnitId:int}")]
    public async Task<IActionResult> GetById(int productUnitId)
    {
        var result = await _productUnitService.GetByIdAsync(productUnitId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int productId, [FromBody] ProductUnitRequest request)
    {
        var result = await _productUnitService.CreateAsync(productId, request);
        return result.IsSuccess
            ? Created()
            : result.ToProblem();
    }

    [HttpPut("{productUnitId:int}")]
    public async Task<IActionResult> Update(int productUnitId, [FromBody] ProductUnitRequest request)
    {
        var result = await _productUnitService.UpdateAsync(productUnitId, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{productUnitId:int}")]
    public async Task<IActionResult> Delete(int productUnitId)
    {
        var result = await _productUnitService.DeleteAsync(productUnitId);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
