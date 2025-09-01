

namespace Newbee.BLL.Services;
internal class CustomerService : ICustomerService
{
    public Task<Result<Customer>> CreateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Customer>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<Customer>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(int id, Customer customer)
    {
        throw new NotImplementedException();
    }
}
