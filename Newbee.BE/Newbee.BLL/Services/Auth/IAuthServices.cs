

using Microsoft.AspNetCore.Identity.Data;
using Newbee.BLL.DTO.Authentication;
using Newbee.BLL.DTO.Mail;
using Newbee.DAL.Abstractions;
using LoginRequest = Newbee.BLL.DTO.Authentication.LoginRequest;
using RegisterRequest = Newbee.BLL.DTO.Authentication.RegisterRequest;

namespace Newbee.BLL.Services.Auth;

public interface IAuthServices
{
    Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(MailRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);

}
