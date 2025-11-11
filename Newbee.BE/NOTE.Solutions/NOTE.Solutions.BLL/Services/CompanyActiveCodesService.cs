using NOTE.Solutions.Entities.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Services;

public class CompanyActiveCodesService(IUnitOfWork unitOfWork) : ICompanyActiveCodesService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly string[] _includes = new string[]
    {
        nameof(Company.ActiveCodeCompanies),
        $"{nameof(Company.ActiveCodeCompanies)}.{nameof(ActiveCodeCompany.ActiveCode)}",
    };
    public async Task<Result> AddActiveCodeToCompanyAsync(int companyId, int activeCodeId, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.FindAsync(x => x.Id == companyId,_includes, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        if (_unitOfWork.ActiveCodes.IsExist(x => x.Id == activeCodeId) is false)
        {
            return Result.Failure(ActiveCodeErrors.NotFound);
        }

        if (company.ActiveCodeCompanies.Any(x => x.ActiveCodeId == activeCodeId))
        {
            return Result.Failure(CompanyActiveCodeErrors.Duplicated);
        }

        await _unitOfWork.ActiveCodeCompanies.AddAsync(new ActiveCodeCompany
        {
            CompanyId = companyId,
            ActiveCodeId = activeCodeId
        });

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RemoveActiveCodeFromCompanyAsync(int companyId, int activeCodeId,CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.FindAsync(x=>x.Id == companyId,_includes,cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        var activeCodeCompany = company.ActiveCodeCompanies.FirstOrDefault(x => x.ActiveCodeId == activeCodeId);

        if(activeCodeCompany is null)
        {
            return Result.Failure(ActiveCodeErrors.NotFound);
        }

        _unitOfWork.ActiveCodeCompanies.Delete(activeCodeCompany);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
