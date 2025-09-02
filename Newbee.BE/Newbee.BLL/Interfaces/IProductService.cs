namespace Newbee.BLL.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<Product>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<Product>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Product>> CreateAsync(int companyId,Product product, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, Product product, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
