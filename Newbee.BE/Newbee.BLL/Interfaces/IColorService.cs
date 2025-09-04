namespace Newbee.BLL.Interfaces;
public interface IColorService
{
    Task<Result<IEnumerable<ColorResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ColorResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ColorResponse>> CreateAsync(ColorRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(int id, ColorRequest request, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
