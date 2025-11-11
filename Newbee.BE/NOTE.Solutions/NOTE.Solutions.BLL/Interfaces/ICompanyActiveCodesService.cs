

namespace NOTE.Solutions.BLL.Interfaces;

public interface ICompanyActiveCodesService
{
    Task<Result> AddActiveCodeToCompanyAsync(int companyId, int activeCodeId, CancellationToken cancellationToken = default);
    Task<Result> RemoveActiveCodeFromCompanyAsync(int companyId, int activeCodeId, CancellationToken cancellationToken = default);
}
