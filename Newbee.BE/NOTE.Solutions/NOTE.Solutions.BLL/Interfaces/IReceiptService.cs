using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.Document.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Interfaces;

public interface IReceiptService
{
    Task<Result<string>> GetNextNumberAsync(int branchId, CancellationToken cancellationToken = default);
    Task<Result<OrderResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(int branchId, CancellationToken cancellationToken = default);
    Task<Result<OrderResponse>> CreateAsync(int branchId,OrderRequest documentRequest, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
