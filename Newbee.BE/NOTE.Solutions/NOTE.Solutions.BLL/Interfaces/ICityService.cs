namespace NOTE.Solutions.BLL.Interfaces;

public interface ICityService
{
    Task<Result<IEnumerable<CityResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CityResponse>>> GetRelatedAsync(int governorateId,CancellationToken cancellationToken = default);
    Task<Result<CityResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<CityResponse>> CreateAsync(CityRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, CityRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
