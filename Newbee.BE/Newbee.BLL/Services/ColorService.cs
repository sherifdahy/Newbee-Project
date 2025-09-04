namespace Newbee.BLL.Services;
public class ColorService : IColorService
{
    public Task<Result<ColorResponse>> CreateAsync(ColorRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<ColorResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ColorResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(int id, ColorRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
