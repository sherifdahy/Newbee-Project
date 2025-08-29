using Api.Bosta.DTOs;
using Api.Bosta.DTOs.Auth.Request;
using Api.Bosta.DTOs.Auth.Response;

namespace Bosta.API.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthDataDTO>> GetAuthTokenAsync(AuthRequestDTO authRequest);
    }
}
