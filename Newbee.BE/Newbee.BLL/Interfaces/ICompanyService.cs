namespace Newbee.BLL.Interfaces;
public interface ICompanyService
{
    Task<Result<IEnumerable<Company>>> GetAllAsync();
    Task<Result<Company>> GetByIdAsync(int id);
    Task<Result<Company>> CreateAsync(Company company);
    Task<Result<bool>> UpdateAsync(int id, Company company);
    Task<Result<bool>> DeleteAsync(int id);
}
