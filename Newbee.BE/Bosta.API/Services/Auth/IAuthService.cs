using Api.Bosta.DTOs;
using Api.Bosta.DTOs.Auth.Request;
using Api.Bosta.DTOs.Auth.Response;
using Bosta.API.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponseDTO<AuthDataDTO>> GetAuthTokenAsync(AuthRequestDTO authRequest);
    }
}
