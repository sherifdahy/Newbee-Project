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
        public async Task<IActionResult> CalculatePrice([FromBody]PricingRequestDTO pricingRequestDTO)
        {
            try
            {
                var response = await _bostaManager.PricingService.PricingCalculator(pricingRequestDTO,_apiKey);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
