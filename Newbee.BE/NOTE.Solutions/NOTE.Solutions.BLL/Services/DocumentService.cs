using NOTE.Solutions.BLL.Contracts.Document.Requests;
using NOTE.Solutions.BLL.Contracts.Document.Responses;

namespace NOTE.Solutions.BLL.Services;

public class DocumentService : IDocumentService
{
    public Task<Result<OrderResponse>> CreateAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<OrderResponse>>> GetAllAsync(int branchId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(int id, OrderRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
