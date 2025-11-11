using NOTE.Solutions.Entities.Entities.Address;

namespace NOTE.Solutions.BLL.Services;

public class CountryService(IUnitOfWork unitOfWork) : ICountryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<CountryResponse>> CreateAsync(CountryRequest request, CancellationToken cancellationToken = default)
    {
        if(_unitOfWork.Countries.IsExist(x => x.Code == request.Code || x.Name == request.Name))
            return Result.Failure<CountryResponse>(CountryErrors.Duplicated);

        var country = request.Adapt<Country>();

        await _unitOfWork.Countries.AddAsync(country, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(country.Adapt<CountryResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(CountryErrors.InvalidId);

        var country = await _unitOfWork.Countries.GetByIdAsync(id, cancellationToken);

        if (country is null)
            return Result.Failure(CountryErrors.NotFound);

        _unitOfWork.Countries.Delete(country);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var countries = await _unitOfWork.Countries.FindAllAsync(x => true,cancellationToken:cancellationToken);

        return Result.Success(countries.Adapt<IEnumerable<CountryResponse>>());
    }
    public async Task<Result<CountryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure<CountryResponse>(CountryErrors.InvalidId);

        var country = await _unitOfWork.Countries.FindAsync(x => x.Id == id,cancellationToken:cancellationToken);

        if (country is null)
            return Result.Failure<CountryResponse>(CountryErrors.NotFound);

        return Result.Success(country.Adapt<CountryResponse>());
    }

    public async Task<Result> UpdateAsync(int id, CountryRequest request, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Result.Failure(CountryErrors.InvalidId);

        if (_unitOfWork.Countries.IsExist(x => (x.Code == request.Code || x.Name == request.Name) && x.Id != id))
            return Result.Failure(CountryErrors.Duplicated); 

        var country = await _unitOfWork.Countries.GetByIdAsync(id,cancellationToken);
        if (country is null)
            return Result.Failure(CountryErrors.NotFound);

        request.Adapt(country);

        _unitOfWork.Countries.Update(country);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
