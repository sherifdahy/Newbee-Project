using NOTE.Solutions.BLL.Contracts.Category.Responses;

namespace NOTE.Solutions.BLL.Interfaces;
public interface ICategoryService
{
    Task<Result<CategoryResponse> >CreateAsync (CategoryRequest request,int BranchId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<bool>>RestoreAsync (int id, CancellationToken cancellationToken = default);
}
