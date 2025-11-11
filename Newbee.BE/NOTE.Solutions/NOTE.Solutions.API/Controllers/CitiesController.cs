namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CitiesController(ICityService cityService) : ControllerBase
{
    private readonly ICityService _cityService = cityService;

    [HttpGet]
    [HasPermission(Permissions.GetCities)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _cityService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("get-related")]
    [HasPermission(Permissions.GetCities)]
    public async Task<IActionResult> GetRelated([FromQuery]int governorateId)
    {
        var result = await _cityService.GetRelatedAsync(governorateId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{cityId:int}")]
    [HasPermission(Permissions.GetCities)]
    public async Task<IActionResult> GetById(int cityId)
    {
        var result = await _cityService.GetByIdAsync(cityId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateCities)]
    public async Task<IActionResult> Create([FromBody] CityRequest request)
    {
        var result = await _cityService.CreateAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { cityId = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{cityId:int}")]
    [HasPermission(Permissions.UpdateCities)]
    public async Task<IActionResult> Update(int cityId, [FromBody] CityRequest request)
    {
        var result = await _cityService.UpdateAsync(cityId, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{cityId:int}")]
    [HasPermission(Permissions.ToggleStatusCities)]
    public async Task<IActionResult> Delete(int cityId)
    {
        var result = await _cityService.DeleteAsync(cityId);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
