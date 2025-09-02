namespace Newbee.BLL.Services;

public class CompanyService(IUnitOfWork unitOfWork) : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Company>> CreateAsync(Company company, CancellationToken cancellationToken = default)
    {
        if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
            return Result.Failure<Company>(CompanyErrors.DuplicatedTRN);

        await _unitOfWork.Companies.AddAsync(company);
        await _unitOfWork.SaveAsync();

        return Result.Success(company);
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id == 0)
            return Result.Failure<bool>(CompanyErrors.InvalidId);

        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(CompanyErrors.NotFound);

        _unitOfWork.Companies.Delete(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<Company>>> GetAllAsync( CancellationToken cancellationToken = default)
    {
        var companies = await _unitOfWork.Companies.GetAllAsync();
        
        return Result.Success(companies);
    }
    public async Task<Result<Company>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<Company>(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.GetByIdAsync(id);
        
        if (company is null)
            return Result.Failure<Company>(CompanyErrors.NotFound);

        return Result.Success(company);
    }
    public async Task<Result<bool>> UpdateAsync(int id, Company company, CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(id);

        if(result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
            return Result.Failure<bool>(CompanyErrors.DuplicatedTRN);

        company.Adapt(result.Value);

        _unitOfWork.Companies.Update(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
}
