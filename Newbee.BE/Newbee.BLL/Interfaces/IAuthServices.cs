using Newbee.BLL.DTO.Auth.Requests;
using Newbee.BLL.DTO.Auth.Responses;

namespace Newbee.BLL.Interfaces;

public interface IAuthServices
{
    Task<Result> RegisterAsync(RegisterCompanyRequest request, CancellationToken cancellationToken = default);
    Task<Result> RegisterAsync(RegisterCustomerRequest request, CancellationToken cancellationToken = default);

    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> ResendConfirmationEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
    Task<Result> SendResetOtpAsync(string email);
}