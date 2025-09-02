using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IProductCategoryService
{
    Task<Result<IEnumerable<ProductCategory>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<ProductCategory>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ProductCategory>> CreateAsync(int companyId, ProductCategory category, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, ProductCategory productCategory, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
