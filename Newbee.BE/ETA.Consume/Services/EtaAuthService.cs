using System.Net.Http.Json;

namespace ETA.Consume.Services;

public class EtaAuthService : IEtaAuthService
{
    private readonly BaseApiCallService _identityApiCall;
    public EtaAuthService(BaseApiCallService identityApiCall)
    {
        _identityApiCall = identityApiCall;
    }

    public async Task<ApiResult<AuthPOSResponse>> AuthenticatePOSAsync(AuthPOSRequest request)
    {
        var headers = new Dictionary<string, string>
        {
            { "posserial", request.POSSerial },
            { "pososversion", "Windows" }
        };

        var values = new Dictionary<string, string>
        {
            { "client_id", request.ClientId },
            { "client_secret", request.ClientSecret },
            { "grant_type", request.GrantType }
        };

        using var content = new FormUrlEncodedContent(values);
        
        return await _identityApiCall.PostReturnAsync<FormUrlEncodedContent, AuthPOSResponse>("connect/token", content, headers);

    }
}
