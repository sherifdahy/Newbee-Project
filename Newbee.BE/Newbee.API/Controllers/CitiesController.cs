using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.City.Requests;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(ICityServices cityServices) : BaseController
    {
        private readonly ICityServices _cityService = cityServices;
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetAllAsync(cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityRequest request, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityRequest request, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }
    }
}
