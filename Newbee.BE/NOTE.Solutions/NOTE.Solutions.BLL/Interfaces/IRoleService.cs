using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Interfaces;
public interface IRoleService  
{
    Task<Result<IEnumerable<RoleResponse>>> GetAllAsync(bool? includeDisabled = false, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<RoleDetailResponse>> CreateAsync(RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleStatus(int id,CancellationToken cancellationToken = default);
}

