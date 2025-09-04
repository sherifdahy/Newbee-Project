using Newbee.BLL.DTO.Platform.Requests;
using Newbee.BLL.DTO.Platform.Responses;
using Newbee.BLL.DTO.Unit.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IUnitService
{
    Task<Result<IEnumerable<UnitResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<UnitResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<UnitResponse>> CreateAsync(PlatformRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, PlatformRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
