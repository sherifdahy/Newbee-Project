namespace Newbee.BLL.Interfaces;
public interface ICustomerService
{
    Task<Result<IEnumerable<Customer>>> GetAllAsync();
    Task<Result<IEnumerable<Customer>>> GetAllAsync(int companyId);
    Task<Result<Customer>> GetByIdAsync(int id);
    Task<Result<Customer>> CreateAsync(Customer customer);
    Task<Result<bool>> UpdateAsync(int id, Customer customer);
    Task<Result<bool>> DeleteAsync(int id);
}
