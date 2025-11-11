namespace ETA.Consume.Contracts.Auth.Requests;

public class AuthPOSRequest
{
    public string POSSerial { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    [JsonProperty("grant_type")]
    public string GrantType { get; set; } = "client_credentials";
}
