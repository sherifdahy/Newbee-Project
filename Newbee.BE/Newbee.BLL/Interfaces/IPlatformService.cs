using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IPlatformService
{
    Task<Result<IEnumerable<Platform>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Platform>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Platform>> CreateAsync(Platform customer, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, Platform customer, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
