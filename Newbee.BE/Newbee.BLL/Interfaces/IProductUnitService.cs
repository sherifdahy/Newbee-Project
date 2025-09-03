using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Interfaces;
public interface IProductUnitService
{
    Task<Result<IEnumerable<ProductUnit>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<ProductUnit>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ProductUnit>> CreateAsync(int companyId, ProductUnit productUnit, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, ProductUnit productUnit, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
