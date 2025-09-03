using Newbee.BLL.DTO.Platform.Requests;
using Newbee.BLL.DTO.Platform.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IPlatformService
{
    Task<Result<IEnumerable<PlatformResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PlatformResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PlatformResponse>> CreateAsync(PlatformRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, PlatformRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
