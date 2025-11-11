using NOTE.Solutions.Entities.Entities.Address;

namespace NOTE.Solutions.BLL.Services;

public class GovernateService(IUnitOfWork unitOfWork) : IGovernateService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task<Result<GovernateResponse>> CreateAsync(GovernateRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Governorates.IsExist(x => (x.Name == request.Name && x.CountryId == request.CountryId) || (x.Code == request.Code)))
            return Result.Failure<GovernateResponse>(GovernateErrors.Duplicated);

        var governate = request.Adapt<Governorate>();

        await _unitOfWork.Governorates.AddAsync(governate,cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(governate.Adapt<GovernateResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(GovernateErrors.InvalidId);

        var governate = await _unitOfWork.Governorates.GetByIdAsync(id,cancellationToken);

        if (governate is null)
            return Result.Failure(GovernateErrors.NotFound);

        _unitOfWork.Governorates.Delete(governate);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<GovernateResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var governates = await _unitOfWork.Governorates.FindAllAsync(x => true,new string[] {nameof(Governorate.Country)}, cancellationToken:cancellationToken);

        return Result.Success(governates.Adapt<IEnumerable<GovernateResponse>>());
    }
    public async Task<Result<IEnumerable<GovernateResponse>>> GetRelatedAsync(int countryId, CancellationToken cancellationToken = default)
    {
        var governates = await _unitOfWork.Governorates.FindAllAsync(x => x.CountryId == countryId, new string[] { nameof(Governorate.Country) }, cancellationToken: cancellationToken);

        return Result.Success(governates.Adapt<IEnumerable<GovernateResponse>>());
    }
    public async Task<Result<GovernateResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<GovernateResponse>(GovernateErrors.InvalidId);

        var governate = await _unitOfWork.Governorates.FindAsync(x => x.Id == id, new string[] { nameof(Governorate.Country) }, cancellationToken: cancellationToken);

        if (governate is null)
            return Result.Failure<GovernateResponse>(GovernateErrors.NotFound);

        return Result.Success(governate.Adapt<GovernateResponse>());
    }

    

    public async Task<Result> UpdateAsync(int id, GovernateRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(GovernateErrors.InvalidId);

        if (_unitOfWork.Governorates.IsExist(x => ((x.CountryId == request.CountryId && x.Name == request.Name) || (x.Code == request.Code)) && x.Id != id))
            return Result.Failure(GovernateErrors.Duplicated);

        var governate = await _unitOfWork.Governorates.GetByIdAsync(id,cancellationToken);
        if (governate is null)
            return Result.Failure(GovernateErrors.NotFound);

        request.Adapt(governate);

        _unitOfWork.Governorates.Update(governate);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
