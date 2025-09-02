namespace Newbee.BLL.Interfaces;
public interface ICustomerService
{
    Task<Result<IEnumerable<Customer>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Customer>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<Customer>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, Customer customer, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
