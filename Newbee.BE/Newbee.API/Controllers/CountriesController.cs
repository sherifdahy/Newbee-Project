using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Company.Responses;
using Newbee.BLL.DTO.Country.Requests;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController(ICountryServices countryServices) : BaseController
    {
        private readonly ICountryServices _countryService = countryServices;
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _countryService.GetAllAsync(cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _countryService.GetByIdAsync(id, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CountryRequest request, CancellationToken cancellationToken)
        {
            var result = await _countryService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _countryService.DeleteAsync(id, cancellationToken);

            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CountryRequest request, CancellationToken cancellationToken)
        {
            var result = await _countryService.CreateAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value.Adapt<CompanyResponse>())
                : result.ToProblem();
        }
    }
}
