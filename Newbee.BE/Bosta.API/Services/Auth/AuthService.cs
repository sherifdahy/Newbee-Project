using Api.Bosta.DTOs;
using Api.Bosta.DTOs.Auth.Request;
using Api.Bosta.DTOs.Auth.Response;
using Api.Bosta.DTOs.Shared;
using Bosta.API.Services.ApiCall;
using System.Net.Http.Json;

namespace Bosta.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IApiCall _apiCall;
        public AuthService(IApiCall apiCall)
        {
            this._apiCall = apiCall;
        }
        public async Task<ApiResponseDTO<AuthDataDTO>> GetAuthTokenAsync(AuthRequestDTO authRequest)
        {
            return await _apiCall.PostAsync<AuthRequestDTO,ApiResponseDTO<AuthDataDTO>>("users/login", authRequest);
        }
    }
}
