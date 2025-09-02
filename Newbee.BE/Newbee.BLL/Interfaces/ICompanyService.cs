namespace Newbee.BLL.Interfaces;
public interface ICompanyService
{
    Task<Result<IEnumerable<Company>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Company>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Company>> CreateAsync(Company company, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, Company company, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
