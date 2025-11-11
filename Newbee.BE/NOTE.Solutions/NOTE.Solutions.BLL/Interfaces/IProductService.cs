
namespace NOTE.Solutions.BLL.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ProductResponse>> CreateAsync(int categoryId, ProductRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, ProductRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
