using NOTE.Solutions.BLL.Contracts.Employee.Requests;
using NOTE.Solutions.BLL.Contracts.Employee.Responses;

namespace NOTE.Solutions.BLL.Interfaces;

public interface IEmployeeService
{
    Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync(int branchId,CancellationToken cancellationToken = default);
    Task<Result<EmployeeResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<EmployeeResponse>> CreateAsync(int branchId,EmployeeRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, EmployeeRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
