using NOTE.Solutions.BLL.Contracts.Employee.Requests;
using NOTE.Solutions.BLL.Contracts.Employee.Responses;
using NOTE.Solutions.Entities.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Services;

public class EmployeeService(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager) : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly string[] _includes = [
        $"{nameof(Employee.ApplicationUser)}",    
    ];

    public async Task<Result<EmployeeResponse>> CreateAsync(int branchId,EmployeeRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Employees.IsExist(x => x.ApplicationUser.Email == request.Email))
            return Result.Failure<EmployeeResponse>(EmployeeErrors.EmailDuplicated);

        if (_unitOfWork.Employees.IsExist(x => x.ApplicationUser.IdentifierNumber == request.IdentifierNumber))
            return Result.Failure<EmployeeResponse>(EmployeeErrors.IdentifierNumberDuplicated);

        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var applicationUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                IdentifierNumber = request.IdentifierNumber,
            };

            var result = await _userManager.CreateAsync(applicationUser, request.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            var employee = new Employee()
            {
                ApplicationUserId = applicationUser.Id,
                BranchEmplyees = [(new BranchEmployee()
                {
                    BranchId = branchId,
                })]
            };

            await _unitOfWork.Employees.AddAsync(employee,cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success(employee.Adapt<EmployeeResponse>());
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _unitOfWork.Employees.FindAsync(x=>x.Id == id,_includes,cancellationToken);

        if (employee is null)
            return Result.Failure(EmployeeErrors.NotFound);

        employee.IsDeleted = true;

        employee.ApplicationUser.IsDeleted = true;

        await _unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync(int branchId,CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.Employees.FindAllAsync(x=> x.BranchEmplyees.Any(x=>x.BranchId == branchId), _includes,cancellationToken);
        return Result.Success(result.Adapt<IEnumerable<EmployeeResponse>>());
    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllInBranchAsync(int branchId,CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.Employees.FindAllAsync(x => x.BranchEmplyees.Any(x => x.BranchId == branchId), _includes, cancellationToken);
        return Result.Success(result.Adapt<IEnumerable<EmployeeResponse>>());
    }

    public async Task<Result<EmployeeResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _unitOfWork.Employees.FindAsync(x => x.Id == id, _includes, cancellationToken);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(EmployeeErrors.NotFound);

        return Result.Success(employee.Adapt<EmployeeResponse>());   
    }

    public async Task<Result> UpdateAsync(int id, EmployeeRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Employees.IsExist(x => x.ApplicationUser.Email == request.Email && x.Id != id))
            return Result.Failure(EmployeeErrors.EmailDuplicated);

        if (_unitOfWork.Employees.IsExist(x => x.ApplicationUser.IdentifierNumber == request.IdentifierNumber && x.Id != id))
            return Result.Failure(EmployeeErrors.IdentifierNumberDuplicated);

        var employee = await _unitOfWork.Employees.FindAsync(x=>x.Id == id, _includes, cancellationToken);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(EmployeeErrors.NotFound);

        employee.ApplicationUser.Email = request.Email;
        employee.ApplicationUser.IdentifierNumber = request.IdentifierNumber;

        return Result.Success();
    }
}
