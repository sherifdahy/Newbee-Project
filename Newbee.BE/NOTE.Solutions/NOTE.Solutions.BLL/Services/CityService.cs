using NOTE.Solutions.Entities.Entities.Address;

namespace NOTE.Solutions.BLL.Services;

public class CityService(IUnitOfWork unitOfWork) : ICityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<CityResponse>> CreateAsync(CityRequest request, CancellationToken cancellationToken = default)
    {
        if (_unitOfWork.Cities.IsExist(x => (x.Name == request.Name && x.GovernorateId == request.GovernorateId) || x.Code == request.Code))
            return Result.Failure<CityResponse>(CityErrors.Duplicated);

        var city = request.Adapt<City>();

        await _unitOfWork.Cities.AddAsync(city, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(city.Adapt<CityResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(CityErrors.InvalidId);

        var city = await _unitOfWork.Cities.GetByIdAsync(id, cancellationToken);

        if (city is null)
            return Result.Failure(CityErrors.NotFound);

        _unitOfWork.Cities.Delete(city);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
    public async Task<Result<IEnumerable<CityResponse>>> GetRelatedAsync(int governorateId, CancellationToken cancellationToken = default)
    {
        var cities = await _unitOfWork.Cities.FindAllAsync(x => x.GovernorateId == governorateId, new string[] { nameof(City.Governorate), $"{nameof(City.Governorate)}.{nameof(Governorate.Country)}" }, cancellationToken: cancellationToken);
        return Result.Success(cities.Adapt<IEnumerable<CityResponse>>());
    }
    public async Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cities = await _unitOfWork.Cities.FindAllAsync(x => true,new string[] {nameof(City.Governorate),$"{nameof(City.Governorate)}.{nameof(Governorate.Country)}"},cancellationToken:cancellationToken);

        return Result.Success(cities.Adapt<IEnumerable<CityResponse>>());
    }

    public async Task<Result<CityResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<CityResponse>(CityErrors.InvalidId);

        var city = await _unitOfWork.Cities.FindAsync(x => x.Id == id, new string[] { nameof(City.Governorate), $"{nameof(City.Governorate)}.{nameof(Governorate.Country)}" }, cancellationToken:cancellationToken);

        if (city is null)
            return Result.Failure<CityResponse>(CityErrors.NotFound);

        return Result.Success(city.Adapt<CityResponse>());
    }

    

    public async Task<Result> UpdateAsync(int id, CityRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(CityErrors.InvalidId);

        if (_unitOfWork.Cities.IsExist(x => ((x.Name == request.Name && x.GovernorateId == request.GovernorateId) || x.Code == request.Code) && x.Id != id))
            return Result.Failure(CityErrors.Duplicated);

        var city = await _unitOfWork.Cities.GetByIdAsync(id, cancellationToken);
        if (city is null)
            return Result.Failure(CityErrors.NotFound);

        request.Adapt(city);

        _unitOfWork.Cities.Update(city);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
