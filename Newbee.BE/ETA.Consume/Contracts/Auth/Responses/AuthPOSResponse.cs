namespace ETA.Consume.Contracts.Auth.Responses;

public class AuthPOSResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
