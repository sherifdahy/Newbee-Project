using Newbee.BLL.DTO.City.Requests;
using Newbee.BLL.DTO.City.Responses;
using Newbee.BLL.Errors;

namespace Newbee.BLL.Services
{
    public class CityServices(IUnitOfWork unitOfWork) : ICityServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<CityResponse>> CreateAsync(CityRequest request, CancellationToken cancellationToken = default)
        {
            var city = request.Adapt<City>();

            // تأكد الدولة موجودة
            var countryExists = _unitOfWork.Countries.IsExist(x => x.Id == request.CountryId);
            if (!countryExists)
                return Result.Failure<CityResponse>(CityErrors.CountryNotFound);

            // تأكد مفيش مدينة بنفس الاسم
            var cityExists = _unitOfWork.Cities.IsExist(x => x.Name == city.Name);
            if (cityExists)
                return Result.Failure<CityResponse>(CityErrors.DuplicatedCity);

            await _unitOfWork.Cities.AddAsync(city);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(city.Adapt<CityResponse>());
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(CityErrors.InvalidId);

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city is null)
                return Result.Failure<bool>(CityErrors.NotFound);

            _unitOfWork.Cities.Delete(city);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var cities = await _unitOfWork.Cities.GetAllAsync();
            return Result.Success(cities.Adapt<IEnumerable<CityResponse>>());
        }

        public async Task<Result<CityResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<CityResponse>(CityErrors.InvalidId);

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city is null)
                return Result.Failure<CityResponse>(CityErrors.NotFound);

            return Result.Success(city.Adapt<CityResponse>());
        }

        public async Task<Result<bool>> UpdateAsync(int id, CityRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(CityErrors.InvalidId);

            var city = await _unitOfWork.Cities.GetByIdAsync(id);
            if (city is null)
                return Result.Failure<bool>(CityErrors.NotFound);

            // تأكد الدولة موجودة
            var countryExists = _unitOfWork.Countries.IsExist(x => x.Id == request.CountryId);
            if (!countryExists)
                return Result.Failure<bool>(CityErrors.CountryNotFound);

            // تأكد مفيش مدينة تانية بنفس الاسم
            var duplicateCity =  _unitOfWork.Cities.IsExist(x => x.Name == request.Name && x.Id != id);
            if (duplicateCity)
                return Result.Failure<bool>(CityErrors.DuplicatedCity);

            request.Adapt(city);
            _unitOfWork.Cities.Update(city);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
