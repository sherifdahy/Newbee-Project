using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NOTE.Solutions.Entities.Abstractions.Consts;
using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Manager;
using System.Linq.Expressions;

namespace NOTE.Solutions.BLL.Services;

public class CompanyService(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : ICompanyService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly string[] _includes =
    {
    };

    public async Task<Result<CompanyResponse>> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Companies.IsExist(x => x.RIN.ToLower() == request.RIN.ToLower()))
            return Result.Failure<CompanyResponse>(CompanyErrors.Duplicated);

        var company = new Company()
        {
            Name = request.Name,
            RIN = request.RIN,
        };

        await _unitOfWork.Companies.AddAsync(company, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Result.Success<CompanyResponse>(company.Adapt<CompanyResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.GetByIdAsync(id, cancellationToken);

        if (company is null)
            return Result.Failure(CompanyErrors.NotFound);

        company.IsDeleted = true;


        foreach(var manager in company.Managers)
        {
            manager.IsDeleted = true;
            manager.ApplicationUser.IsDeleted = true;
        }

        foreach(var branch in company.Branches)
        {
            branch.IsDeleted = true;

            foreach(var branchEmployee in branch.BranchEmplyees)
            {
                _unitOfWork.BranchEmployees.Delete(branchEmployee);
            }
        }

        _unitOfWork.Companies.Update(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<CompanyResponse>>> GetAllAsync(int? userId, CancellationToken cancellationToken = default)
    {
        Expression<Func<Company, bool>> query = null!;

        if (userId.HasValue)
        {
            query = x => x.Branches.Any(uc => uc.BranchEmplyees.Any(x => x.Employee.ApplicationUserId == userId.Value));
        }
        else
        {
            query = x => true;
        }

        var companies = await _unitOfWork.Companies.FindAllAsync(query!, _includes, cancellationToken);
        return Result.Success(companies.Adapt<IEnumerable<CompanyResponse>>());
    }

    public async Task<Result<CompanyResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<CompanyResponse>(CompanyErrors.InvalidId);

        var company = await _unitOfWork.Companies.FindAsync(x => x.Id == id, _includes, cancellationToken);

        if (company is null)
            return Result.Failure<CompanyResponse>(CompanyErrors.NotFound);

        return Result.Success(company.Adapt<CompanyResponse>());
    }

    public async Task<Result> UpdateAsync(int id, UpdateCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.FindAsync(x => x.Id == id, _includes, cancellationToken);

        if (company is null)
            return Result.Failure<CompanyResponse>(CompanyErrors.NotFound);

        if (_unitOfWork.Companies.IsExist(x => (x.RIN == request.RIN) && x.Id != id))
            return Result.Failure<CompanyResponse>(CompanyErrors.Duplicated);

        company.Name = request.Name;
        company.RIN = request.RIN;

        _unitOfWork.Companies.Update(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();    
    }
}
