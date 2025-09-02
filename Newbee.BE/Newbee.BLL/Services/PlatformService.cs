using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Services;
public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Platform>> CreateAsync(Platform platform, CancellationToken cancellationToken = default)
    {

        await _unitOfWork.Platforms.AddAsync(platform);
        await _unitOfWork.SaveAsync();

        return Result.Success(platform);
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        _unitOfWork.Platforms.Delete(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<Platform>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var platforms = await _unitOfWork.Platforms.GetAllAsync();

        return Result.Success(platforms);
    }
    public async Task<Result<Platform>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<Platform>(PlatformErrors.InvalidId);

        var platform = await _unitOfWork.Platforms.FindAsync(x => x.Id == id);

        if (platform is null)
            return Result.Failure<Platform>(PlatformErrors.NotFound);

        return Result.Success(platform);
    }
    public async Task<Result<bool>> UpdateAsync(int id, Platform platform, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(PlatformErrors.InvalidId);

        var result = await GetByIdAsync(id);

        if (!result.IsSuccess)
            return Result.Failure<bool>(result.Error);

        platform.Adapt(result.Value);

        _unitOfWork.Platforms.Update(result.Value);
        await _unitOfWork.SaveAsync();

        return Result.Success(true);
    }
}
