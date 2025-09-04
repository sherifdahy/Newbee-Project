using Newbee.BLL.DTO.City.Requests;
using Newbee.BLL.DTO.City.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces
{
    public interface ICityServices
    {
        Task<Result<CityResponse>> CreateAsync(CityRequest request, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<CityResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<bool>> UpdateAsync(int id, CityRequest request, CancellationToken cancellationToken = default);
    }

}
