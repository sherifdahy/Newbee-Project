using Mapster;
using Newbee.BLL.DTO.Zone.Requests;
using Newbee.BLL.DTO.Zone.Responses;


namespace Newbee.BLL.Services
{
    public class ZoneServices(IUnitOfWork unitOfWork) : IZoneServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<ZoneResponse>> CreateAsync(ZoneRequest request, CancellationToken cancellationToken = default)
        {
            // Check if City exists
            var cityExists = await _unitOfWork.Cities.GetByIdAsync(request.CityId);
            if (cityExists is null)
                return Result.Failure<ZoneResponse>(ZoneErrors.CityNotFound);

            // Check if Zone already exists
            if (_unitOfWork.Zones.IsExist(z => z.Name == request.Name))
                return Result.Failure<ZoneResponse>(ZoneErrors.DuplicatedZone);

            var zone = request.Adapt<Zone>();
            await _unitOfWork.Zones.AddAsync(zone);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(zone.Adapt<ZoneResponse>());
        }

        public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(ZoneErrors.InvalidId);

            var zone = await _unitOfWork.Zones.GetByIdAsync(id);
            if (zone is null)
                return Result.Failure<bool>(ZoneErrors.NotFound);

            _unitOfWork.Zones.Delete(zone);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<ZoneResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var zones = await _unitOfWork.Zones.GetAllAsync();
            return Result.Success(zones.Adapt<IEnumerable<ZoneResponse>>());
        }

        public async Task<Result<ZoneResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<ZoneResponse>(ZoneErrors.InvalidId);

            var zone = await _unitOfWork.Zones.GetByIdAsync(id);
            if (zone is null)
                return Result.Failure<ZoneResponse>(ZoneErrors.NotFound);

            return Result.Success(zone.Adapt<ZoneResponse>());
        }

        public async Task<Result<bool>> UpdateAsync(int id, ZoneRequest request, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                return Result.Failure<bool>(ZoneErrors.InvalidId);

            var zone = await _unitOfWork.Zones.GetByIdAsync(id);
            if (zone is null)
                return Result.Failure<bool>(ZoneErrors.NotFound);

            // Check City exists
            var cityExists = await _unitOfWork.Cities.GetByIdAsync(request.CityId);
            if (cityExists is null)
                return Result.Failure<bool>(ZoneErrors.CityNotFound);

            // Check duplicate zone name (excluding current)
            if (_unitOfWork.Zones.IsExist(z => z.Name == request.Name && z.Id != id))
                return Result.Failure<bool>(ZoneErrors.DuplicatedZone);

            request.Adapt(zone);
            _unitOfWork.Zones.Update(zone);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(true);
        }
    }
}
