
namespace NOTE.Solutions.BLL.Interfaces;

public interface IActiveCodesService
{
    Task<Result<IEnumerable<ActiveCodeResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ActiveCodeResponse>> GetById(int id, CancellationToken cancellationToken = default);
    Task<Result<ActiveCodeResponse>> CreateAsync(ActiveCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, ActiveCodeRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
