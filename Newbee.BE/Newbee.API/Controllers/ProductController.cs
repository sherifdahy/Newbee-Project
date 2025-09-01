using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.API.Abstractions;
using Newbee.BLL.Interfaces;
using System.Threading.Tasks;

namespace Newbee.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) :BaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync(CompanyId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
