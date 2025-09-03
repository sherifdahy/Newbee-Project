
namespace Newbee.BLL.DTO.Auth.Requests;

public record RefreshTokenRequest(
 string Token,
 string RefreshToken
);
