using Newbee.BLL.DTO.Company.Requests;
using Newbee.BLL.DTO.Company.Responses;

namespace Newbee.BLL.Services;

public class CompanyService(IUnitOfWork unitOfWork) : ICompanyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CompanyResponse>> CreateAsync(CompanyRequest request, CancellationToken cancellationToken = default)
    {
        var company = request.Adapt<Company>();
        
        if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == company.TaxRegistrationNumber))
            return Result.Failure<CompanyResponse>(CompanyErrors.DuplicatedTRN);


        await _unitOfWork.Companies.AddAsync(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(company.Adapt<CompanyResponse>());
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if(id == 0)
            return Result.Failure<bool>(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.GetByIdAsync(id);

        if (company is null)
            return Result.Failure<bool>(CompanyErrors.NotFound);

        _unitOfWork.Companies.Delete(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<CompanyResponse>>> GetAllAsync( CancellationToken cancellationToken = default)
    {
        var companies = await _unitOfWork.Companies.GetAllAsync();
        
        return Result.Success(companies.Adapt<IEnumerable<CompanyResponse>>());
    }
    public async Task<Result<CompanyResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<CompanyResponse>(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.GetByIdAsync(id);
        
        if (company is null)
            return Result.Failure<CompanyResponse>(CompanyErrors.NotFound);

        return Result.Success(company.Adapt<CompanyResponse>());
    }
    public async Task<Result<bool>> UpdateAsync(int id, CompanyRequest request, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.GetByIdAsync(id);

        if(company is null)
            return Result.Failure<bool>(CompanyErrors.NotFound);

        if (!_unitOfWork.Companies.IsExist(x => x.TaxRegistrationNumber == request.TaxRegistrationNumber))
            return Result.Failure<bool>(CompanyErrors.DuplicatedTRN);

        request.Adapt(company);

        _unitOfWork.Companies.Update(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
}
