using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.Document.Responses;
using NOTE.Solutions.BLL.Contracts.OrderLine.Requests;

namespace NOTE.Solutions.BLL.Interfaces;

public interface IOrderService
{
    // Basic Document/Order CRUD Operations
    Task<Result<OrderResponse>> CreateAsync(OrderRequest request, int companyId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(int companyId, CancellationToken cancellationToken = default);
    Task<Result<OrderResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, OrderRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<bool>> RestoreAsync(int id, CancellationToken cancellationToken = default);
   
}