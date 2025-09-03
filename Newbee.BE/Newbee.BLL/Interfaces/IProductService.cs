using Newbee.BLL.DTO.Product.Requests;
using Newbee.BLL.DTO.Product.Responses;

namespace Newbee.BLL.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ProductResponse>> CreateAsync(int companyId,ProductRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, ProductRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
