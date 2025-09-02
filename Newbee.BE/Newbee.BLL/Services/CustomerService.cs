

namespace Newbee.BLL.Services;
public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Customer>> CreateAsync(Customer customer)
    {
        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveAsync();

        return Result.Success(customer);
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(CustomerErrors.NotFound);

        _unitOfWork.Customers.Delete(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<Customer>>> GetAllAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();

        return Result.Success(customers);
    }

    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(int companyId)
    {
        if (companyId == 0)
            return Result.Failure<IEnumerable<Customer>>(CompanyErrors.InvalidId);

        if (!_unitOfWork.Companies.IsExist(x => x.Id == companyId))
            return Result.Failure<IEnumerable<Customer>>(CompanyErrors.NotFound);

        var customers = await _unitOfWork.Customers.FindAllAsync(x => x.CompanyId == companyId);

        return Result.Success(customers);
    }

    public async Task<Result<Customer>> GetByIdAsync(int id)
    {
        if (id == 0)
            return Result.Failure<Customer>(CustomerErrors.InvalidId);

        var customer = await _unitOfWork.Customers.FindAsync(x => x.Id == id);

        return Result.Success(customer);
    }

    public async Task<Result<bool>> UpdateAsync(int id, Customer customer)
    {
        if (id == 0)
            return Result.Failure<bool>(CustomerErrors.InvalidId);

        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        customer.Adapt(result.Value);

        _unitOfWork.Customers.Update(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
}
