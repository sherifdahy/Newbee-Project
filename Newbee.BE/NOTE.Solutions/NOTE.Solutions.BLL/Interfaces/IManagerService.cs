using NOTE.Solutions.BLL.Contracts.Employee.Responses;
using NOTE.Solutions.BLL.Contracts.Manager.Requests;
using NOTE.Solutions.BLL.Contracts.Manager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Interfaces;

public interface IManagerService
{
    Task<Result<ManagerResponse>> CreateAsync(int companyId, ManagerRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ManagerResponse>>> GetAllAsync(int? companyId=null, CancellationToken cancellationToken = default);
    Task<Result<ManagerResponse>> GetByIdAsync(int managerId, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int companyId,int managerId, ManagerRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int managerId, CancellationToken cancellationToken = default);
}
