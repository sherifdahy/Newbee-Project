using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) :BaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync(CompanyId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
