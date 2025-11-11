using ETA.Consume.Abstractions;

namespace ETA.Consume.Interfaces;

public interface IEtaAuthService
{
    Task<ApiResult<AuthPOSResponse>> AuthenticatePOSAsync(AuthPOSRequest request);
}
