
using Newbee.BLL.DTO.Auth.Requests;
using Newbee.BLL.DTO.Auth.Responses;
using Newbee.BLL.DTO.Mail;

namespace Newbee.BLL.Interfaces;

public interface IAuthServices
{
    Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(MailRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);

}
