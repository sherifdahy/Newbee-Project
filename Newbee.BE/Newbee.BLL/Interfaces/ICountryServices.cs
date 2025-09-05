using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Company.Responses;
using Newbee.BLL.DTO.Country.Requests;
using Newbee.BLL.DTO.Country.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces
{
    public interface ICountryServices
    {
        Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<CountryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<CountryResponse>> CreateAsync(CountryRequest company, CancellationToken cancellationToken = default);
        Task<Result<bool>> UpdateAsync(int id, CountryRequest company, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
