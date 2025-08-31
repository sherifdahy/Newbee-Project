using Bosta.API.DTOs.Price;
using Bosta.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : BaseController
    {
        public PricingController(IBostaManager bostaManager) : base (bostaManager)
        {
        }

        [HttpPost]
        public async Task<IActionResult> PricingCalculator([FromBody] PricingRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return BadRequest(errors);
                }

                var result = await _bostaManager.PricingService.PricingCalculator(request, _apiKey);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
