namespace NOTE.Solutions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyActiveCodesController(ICompanyActiveCodesService companyActiveCodesService) : ControllerBase
    {
        private readonly ICompanyActiveCodesService _companyActiveCodesService = companyActiveCodesService;

        [HttpPost("/api/companies/{companyId:int}/activecodes/{activeCodeId:int}")]
        public async Task<IActionResult> AddActiveCodeToCompany(int companyId, int activeCodeId, CancellationToken cancellationToken = default)
        {
            var result = await _companyActiveCodesService.AddActiveCodeToCompanyAsync(companyId, activeCodeId, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(AddActiveCodeToCompany), new { companyId, activeCodeId }, null) : result.ToProblem();
        }

        [HttpDelete("/api/companies/{companyId:int}/activecodes/{activeCodeId:int}")]
        public async Task<IActionResult> RemoveActiveCodeFromCompany(int companyId, int activeCodeId, CancellationToken cancellationToken = default)
        {
            var result = await _companyActiveCodesService.RemoveActiveCodeFromCompanyAsync(companyId, activeCodeId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
