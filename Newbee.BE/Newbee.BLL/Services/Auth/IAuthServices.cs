

using Newbee.BLL.DTO.Authentication;
using Newbee.DAL.Abstractions;

namespace Newbee.BLL.Services.Auth;

public interface IAuthServices
{
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

}
