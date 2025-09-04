using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newbee.BLL.DTO.Zone.Requests;
using Newbee.BLL.Interfaces;

namespace Newbee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController(IZoneServices zoneServices) : BaseController
    {
        private readonly IZoneServices _zoneService = zoneServices;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _zoneService.GetAllAsync(cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _zoneService.GetByIdAsync(id, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ZoneRequest request, CancellationToken cancellationToken)
        {
            var result = await _zoneService.CreateAsync(request, cancellationToken);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ZoneRequest request, CancellationToken cancellationToken)
        {
            var result = await _zoneService.UpdateAsync(id, request, cancellationToken);
            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _zoneService.DeleteAsync(id, cancellationToken);
            return result.IsSuccess
                ? NoContent()
                : result.ToProblem();
        }
    }
}
