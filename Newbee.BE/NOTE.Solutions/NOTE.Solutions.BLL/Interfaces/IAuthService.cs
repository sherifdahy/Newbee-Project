using NOTE.Solutions.BLL.Contracts.Auth.Requests;
using NOTE.Solutions.BLL.Contracts.Auth.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token,string refreshToken,CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetTokenAsync(LoginRequest authRequest, CancellationToken cancellationToken = default);
    Task<Result<bool>> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken cancellationToken = default);
    Task<Result> RevokeAsync(string token,string refreshToken, CancellationToken cancellationToken = default);
}
