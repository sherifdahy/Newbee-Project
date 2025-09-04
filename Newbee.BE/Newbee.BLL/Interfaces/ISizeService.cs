namespace Newbee.BLL.Interfaces;
public interface ISizeService
{
    Task<Result<IEnumerable<SizeResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<SizeResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<SizeResponse>> CreateAsync(SizeRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, SizeRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
