using Bosta.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupLocationsController : BaseController
    {
        public PickupLocationsController(IBostaManager bostaManager) : base(bostaManager)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var locationsResult = await _bostaManager.PickupLocationsService.GetAllAsync(_apiKey);

                return Ok(locationsResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var locationResult = await _bostaManager.PickupLocationsService.GetByIdAsync(id,_apiKey);
                return Ok(locationResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
