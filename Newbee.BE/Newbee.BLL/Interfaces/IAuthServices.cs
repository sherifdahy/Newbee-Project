using Newbee.BLL.DTO.Auth.Requests;
using Newbee.BLL.DTO.Auth.Responses;

namespace Newbee.BLL.Interfaces;

public interface IAuthServices
{
    Task<Result> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken cancellationToken = default);
    Task<Result> RegisterCustomerAsync(RegisterCustomerRequest request, Guid apiKey, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
<<<<<<< HEAD
=======
    Task<bool> RevokedRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

>>>>>>> d8396f1e8a19c352a3f4ce797f9913b100ef5c2b
    Task<Result> ResendConfirmationEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
    Task<Result> SendResetOtpAsync(string email);
}