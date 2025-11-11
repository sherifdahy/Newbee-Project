using NOTE.Solutions.BLL.Contracts.Manager.Requests;
using NOTE.Solutions.BLL.Contracts.Manager.Responses;
using NOTE.Solutions.Entities.Entities.Manager;
using System.Linq.Expressions;

namespace NOTE.Solutions.BLL.Services;

public class ManagerService(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : IManagerService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly string[] _includes = new string[]
    {
        nameof(Manager.ApplicationUser)
    };

    public async Task<Result<ManagerResponse>> CreateAsync(int companyId, ManagerRequest request, CancellationToken cancellationToken = default)
    {
        var company = await _unitOfWork.Companies.FindAsync(x => x.Id == companyId, null, cancellationToken);

        if (company is null)
            return Result.Failure<ManagerResponse>(CompanyErrors.NotFound);

        if (_unitOfWork.Managers.IsExist(x => x.ApplicationUser.Email == request.Email))
            return Result.Failure<ManagerResponse>(ManagerErrors.EmailDuplicated);

        if (_unitOfWork.Managers.IsExist(x => (x.ApplicationUser.IdentifierNumber == request.IdentifierNumber) && (x.CompanyId == companyId)))
            return Result.Failure<ManagerResponse>(ManagerErrors.EmailDuplicated);

        var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var applicationUser = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.Email,
                IdentifierNumber = request.IdentifierNumber,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(applicationUser, request.Password);

            if (result.Succeeded)
            {
                var manager = new Manager()
                {
                    CompanyId = companyId,
                    ApplicationUser = applicationUser
                };

                await _unitOfWork.Managers.AddAsync(manager, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result.Success<ManagerResponse>(manager.Adapt<ManagerResponse>());

            }
            var error = result.Errors.First();
            return Result.Failure<ManagerResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Result> DeleteAsync(int managerId, CancellationToken cancellationToken = default)
    {
        var manager = await _unitOfWork.Managers.FindAsync(x => x.Id == managerId, _includes, cancellationToken);

        if (manager is null)
            return Result.Failure(ManagerErrors.NotFound);

        manager.IsDeleted = true;
        manager.ApplicationUser.IsDeleted = true;

        _unitOfWork.Managers.Update(manager);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ManagerResponse>>> GetAllAsync(int? companyId= null, CancellationToken cancellationToken = default)
    {
        Expression<Func<Manager, bool>>? query = null;

        if (companyId.HasValue)
        {
            if (_unitOfWork.Companies.IsExist(x => x.Id == companyId) is false)
                return Result.Failure<IEnumerable<ManagerResponse>>(CompanyErrors.NotFound);

            query = x => x.CompanyId == companyId;
        }
        else
        {
            query = x => true;
        }

        var managers = await _unitOfWork.Managers.FindAllAsync(query, _includes, cancellationToken);
        
        return Result.Success(managers.Adapt<IEnumerable<ManagerResponse>>());
    }

    public async Task<Result<ManagerResponse>> GetByIdAsync(int managerId, CancellationToken cancellationToken = default)
    {
        var manager = await _unitOfWork.Managers.FindAsync(x => x.Id == managerId, _includes, cancellationToken);

        if (manager is null)
            return Result.Failure<ManagerResponse>(ManagerErrors.NotFound);

        return Result.Success(manager.Adapt<ManagerResponse>());
    }

    public async Task<Result> UpdateAsync(int companyId, int managerId, ManagerRequest request, CancellationToken cancellationToken = default)
    {
        var manager = await _unitOfWork.Managers.FindAsync(x => x.Id == managerId, _includes, cancellationToken);

        if (manager is null)
            return Result.Failure<ManagerResponse>(ManagerErrors.NotFound);

        if (_unitOfWork.Managers.IsExist(x => x.ApplicationUser.Email == request.Email && x.Id != manager.Id))
            return Result.Failure<ManagerResponse>(ManagerErrors.EmailDuplicated);

        if (_unitOfWork.Managers.IsExist(x => (x.ApplicationUser.IdentifierNumber == request.IdentifierNumber) && x.CompanyId == companyId && x.Id != managerId))
            return Result.Failure<ManagerResponse>(ManagerErrors.IdentifierNumberDuplicated);

        manager.ApplicationUser.UserName = request.Email;
        manager.ApplicationUser.Email = request.Email;
        manager.ApplicationUser.Name = request.Name;
        manager.ApplicationUser.IdentifierNumber = request.IdentifierNumber;
        manager.ApplicationUser.PhoneNumber = request.PhoneNumber;
        manager.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(manager.ApplicationUser, request.Password);

        var result = await _userManager.UpdateAsync(manager.ApplicationUser);

        if(!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        return Result.Success();
    }
}
