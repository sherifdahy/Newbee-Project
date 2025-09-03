using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Company.Responses;

namespace Newbee.BLL.Interfaces;
public interface ICompanyService
{
    Task<Result<IEnumerable<CompanyResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<CompanyResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<CompanyResponse>> CreateAsync(CompanyRequest company, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, CompanyRequest company, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
