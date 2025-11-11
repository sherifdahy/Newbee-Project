namespace NOTE.Solutions.BLL.Interfaces;

public interface ICountryService
{
    Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<CountryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<CountryResponse>> CreateAsync(CountryRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, CountryRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
