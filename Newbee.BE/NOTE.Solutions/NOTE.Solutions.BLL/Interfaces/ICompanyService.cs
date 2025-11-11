namespace NOTE.Solutions.BLL.Interfaces;
public interface ICompanyService
{
    Task<Result<IEnumerable<CompanyResponse>>> GetAllAsync(int? userId, CancellationToken cancellationToken = default);
    Task<Result<CompanyResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<CompanyResponse>> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, UpdateCompanyRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
