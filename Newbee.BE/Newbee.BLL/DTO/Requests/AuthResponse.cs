namespace Newbee.BLL.DTO.Authentication;
public record AuthResponse
(
    int  UserId,
    string Email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn,
    string RefreshToken ,
    DateTime RefreshTokenExpiration

);

