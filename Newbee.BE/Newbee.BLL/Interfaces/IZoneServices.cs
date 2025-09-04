using Newbee.BLL.DTO.Zone.Requests;
using Newbee.BLL.DTO.Zone.Responses;

namespace Newbee.BLL.Interfaces
{
    public interface IZoneServices
    {
        Task<Result<ZoneResponse>> CreateAsync(ZoneRequest request, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ZoneResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<ZoneResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<bool>> UpdateAsync(int id, ZoneRequest request, CancellationToken cancellationToken = default);
    }
}
