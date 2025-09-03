using Newbee.BLL.DTO.ProductCategory.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IProductCategoryService
{
    Task<Result<IEnumerable<ProductCategoryResponse>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<ProductCategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ProductCategoryResponse>> CreateAsync(int companyId, ProductCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, ProductCategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
