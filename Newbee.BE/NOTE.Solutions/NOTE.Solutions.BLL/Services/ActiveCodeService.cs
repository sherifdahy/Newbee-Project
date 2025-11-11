using Microsoft.AspNetCore.Http;
using NOTE.Solutions.API.Extensions;
using NOTE.Solutions.Entities.Entities.Company;

namespace NOTE.Solutions.BLL.Services;

public class ActiveCodeService : IActiveCodesService
{
    private string _cachedKey;
    private int _userId;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ActiveCodeService(IUnitOfWork unitOfWork,ICacheService cacheService,IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
        _httpContextAccessor = httpContextAccessor;

        _userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        _cachedKey = $"activecodes_user_{_userId}";

    }
    public async Task<Result<ActiveCodeResponse>> CreateAsync(ActiveCodeRequest request, CancellationToken cancellationToken = default)
    {
        if(_unitOfWork.ActiveCodes.IsExist(x => x.Code == request.Code))
            return Result.Failure<ActiveCodeResponse>(ActiveCodeErrors.Duplicated);

        var activeCode = request.Adapt<ActiveCode>();

        await _unitOfWork.ActiveCodes.AddAsync(activeCode,cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey, cancellationToken);

        return Result.Success(activeCode.Adapt<ActiveCodeResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ActiveCodeErrors.InvalidId);

        var activeCode = await _unitOfWork.ActiveCodes.GetByIdAsync(id, cancellationToken);

        if (activeCode is null)
            return Result.Failure(ActiveCodeErrors.NotFound);

        _unitOfWork.ActiveCodes.Delete(activeCode);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ActiveCodeResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cachedActiveCodes = await _cacheService.GetAsync<IEnumerable<ActiveCodeResponse>>(_cachedKey);

        if (cachedActiveCodes is not null)
            return Result.Success(cachedActiveCodes);

        var activeCodes = await _unitOfWork.ActiveCodes.FindAllAsync(x => true, cancellationToken: cancellationToken);

        await _cacheService.SetAsync(_cachedKey, activeCodes.Adapt<IEnumerable<ActiveCodeResponse>>(), TimeSpan.FromDays(10));

        return Result.Success(activeCodes.Adapt<IEnumerable<ActiveCodeResponse>>());
    }
    public async Task<Result<ActiveCodeResponse>> GetById(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<ActiveCodeResponse>(ActiveCodeErrors.InvalidId);

        var activeCode = await _unitOfWork.ActiveCodes.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);

        if (activeCode is null)
            return Result.Failure<ActiveCodeResponse>(ActiveCodeErrors.NotFound);

        return Result.Success(activeCode.Adapt<ActiveCodeResponse>());
    }

    public async Task<Result> UpdateAsync(int id, ActiveCodeRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(ActiveCodeErrors.InvalidId);

        var activeCode = await _unitOfWork.ActiveCodes.GetByIdAsync(id,cancellationToken);
        if (activeCode is null)
            return Result.Failure(ActiveCodeErrors.NotFound);

        request.Adapt(activeCode);

        _unitOfWork.ActiveCodes.Update(activeCode);
        await _unitOfWork.SaveAsync(cancellationToken);

        await _cacheService.RemoveAsync(_cachedKey, cancellationToken);

        return Result.Success();
    }
}
