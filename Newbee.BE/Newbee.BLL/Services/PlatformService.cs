using Newbee.BLL.DTO.Platform.Requests;
using Newbee.BLL.DTO.Platform.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Services;
public class PlatformService(IUnitOfWork unitOfWork) : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PlatformResponse>> CreateAsync(PlatformRequest request, CancellationToken cancellationToken = default)
    {
        var platform = request.Adapt<Platform>();
        await _unitOfWork.Platforms.AddAsync(platform);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(platform.Adapt<PlatformResponse>());
    }
    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(PlatformErrors.InvalidId);

        var platform = await _unitOfWork.Platforms.GetByIdAsync(id);

        if (platform is null)
            return Result.Failure<bool>(PlatformErrors.NotFound);

        _unitOfWork.Platforms.Delete(platform);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
    public async Task<Result<IEnumerable<PlatformResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var platforms = await _unitOfWork.Platforms.GetAllAsync();

        return Result.Success(platforms.Adapt<IEnumerable<PlatformResponse>>());
    }
    public async Task<Result<PlatformResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<PlatformResponse>(PlatformErrors.InvalidId);

        var platform = await _unitOfWork.Platforms.FindAsync(x => x.Id == id);

        if (platform is null)
            return Result.Failure<PlatformResponse>(PlatformErrors.NotFound);

        return Result.Success(platform.Adapt<PlatformResponse>());
    }
    public async Task<Result<bool>> UpdateAsync(int id, PlatformRequest request, CancellationToken cancellationToken = default)
    {
        if (id == 0)
            return Result.Failure<bool>(PlatformErrors.InvalidId);

        var platform= await _unitOfWork.Platforms.GetByIdAsync(id);

        if (platform is null)
            return Result.Failure<bool>(PlatformErrors.NotFound);

        request.Adapt(platform);

        _unitOfWork.Platforms.Update(platform);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(true);
    }
}
