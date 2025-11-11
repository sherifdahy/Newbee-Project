using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NOTE.Solutions.BLL.Contracts.Employee.Requests;

namespace NOTE.Solutions.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpPost("/api/branches/{branchId}/employees")]
    public async Task<IActionResult> Create(int branchId,EmployeeRequest request,CancellationToken cancellationToken)
    {
        var result = await _employeeService.CreateAsync(branchId,request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpGet("/api/branches/{branchId}/employees")]
    public async Task<IActionResult> GetAll(int branchId,CancellationToken cancellationToken)
    {
        var result = await _employeeService.GetAllAsync(branchId,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id ,CancellationToken cancellationToken)
    {
        var result = await _employeeService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(); 
    }



    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
    {
        var result = await _employeeService.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
