namespace Newbee.BLL.Services;
public class CompanyService : ICompanyService
{
    public Task<Result<Company>> CreateAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Company>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Company>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(int id, Company company)
    {
        throw new NotImplementedException();
    }
}
