namespace NOTE.Solutions.BLL.Interfaces;

public interface IUnitService
{
    Task<Result<IEnumerable<UnitResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<UnitResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<UnitResponse>> CreateAsync(UnitRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, UnitRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
