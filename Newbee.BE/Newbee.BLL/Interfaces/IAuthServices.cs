using Newbee.BLL.DTO.Requests;

namespace Newbee.BLL.Interfaces;

public interface IAuthServices
{
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

}
