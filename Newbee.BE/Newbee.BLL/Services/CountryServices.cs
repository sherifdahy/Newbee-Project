using Azure.Core;
using Newbee.BLL.DTO.Company.Responses;
using Newbee.BLL.DTO.Country.Requests;
using Newbee.BLL.DTO.Country.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Services
{
    public class CountryServices(IUnitOfWork unitOfWork) : ICountryServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<CountryResponse>> CreateAsync(CountryRequest company, CancellationToken cancellationToken = default)
        {
            var country = company.Adapt<Country>();
            if (!_unitOfWork.Countries.IsExist(x => x.Name == country.Name))
                 return Result.Failure<CountryResponse>(CountryErrors.DuplicatedCountry);
            await _unitOfWork.Countries.AddAsync(country);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success(country.Adapt<CountryResponse>());
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(CountryErrors.InvalidId);

            var country = await _unitOfWork.Countries.GetByIdAsync(id);

            if (country is null)
                return Result.Failure<bool>(CountryErrors.NotFound);

            _unitOfWork.Countries.Delete(country);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var countries = await  _unitOfWork.Countries.GetAllAsync();

            return Result.Success(countries.Adapt<IEnumerable<CountryResponse>>());
        }

        public async Task<Result<CountryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<CountryResponse>(CountryErrors.InvalidId);

            var country = await _unitOfWork.Countries.GetByIdAsync(id);

            if (country is null)
                return Result.Failure<CountryResponse>(CountryErrors.NotFound);

            return Result.Success(country.Adapt<CountryResponse>());
        }

        public async Task<Result<bool>> UpdateAsync(int id, CountryRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(CountryErrors.InvalidId);

            var country = await _unitOfWork.Countries.GetByIdAsync(id);

            if (country is null)
                return Result.Failure<bool>(CountryErrors.NotFound);

            if (!_unitOfWork.Countries.IsExist(x => x.Code == request.Code && x.Id != id))
                return Result.Failure<bool>(CountryErrors.DuplicatedCountry);

            request.Adapt(country);

            _unitOfWork.Countries.Update(country);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
