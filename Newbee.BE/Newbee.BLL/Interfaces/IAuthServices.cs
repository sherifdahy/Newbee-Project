using Newbee.BLL.DTO.Requests;

<<<<<<< HEAD:Newbee.BE/Newbee.BLL/Interfaces/IAuthServices.cs
namespace Newbee.BLL.Interfaces;
=======
using Microsoft.AspNetCore.Identity.Data;
using Newbee.BLL.DTO.Authentication;
using Newbee.BLL.DTO.Mail;
using Newbee.DAL.Abstractions;
using LoginRequest = Newbee.BLL.DTO.Authentication.LoginRequest;
using RegisterRequest = Newbee.BLL.DTO.Authentication.RegisterRequest;

namespace Newbee.BLL.Services.Auth;
>>>>>>> 2282cb2f709b636a98913456b9f6df9a366c7de9:Newbee.BE/Newbee.BLL/Services/Auth/IAuthServices.cs

public interface IAuthServices
{
    Task<Result> RegisterMerchantAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(MailRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse?>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);

}
