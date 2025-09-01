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

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync(CompanyId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    //[HttpGet("{id:int}")]
    //public async Task<IActionResult> GetById(int id)
    //{
    //    var result = await _productService.GetByIdAsync(id, CompanyId);
    //    return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    //}
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        var result = await _productService.UpdateAsync(id, product);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        var result = await _productService.CreateAsync(product);
        return result.IsSuccess ? CreatedAtAction(nameof(GetAll), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }
}
