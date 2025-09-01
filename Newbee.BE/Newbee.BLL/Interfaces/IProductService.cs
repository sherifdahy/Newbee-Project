namespace Newbee.BLL.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<Product>>> GetAllAsync(int companyId);
    Task<Result<Product>> GetByIdAsync(int id);
    Task<Result<Product>> CreateAsync(Product product);
    Task<Result<bool>> UpdateAsync(int id, Product product);
    Task<Result<bool>> DeleteAsync(int id);
}
