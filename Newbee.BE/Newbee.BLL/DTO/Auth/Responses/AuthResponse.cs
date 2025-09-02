namespace Newbee.BLL.DTO.Auth.Responses;

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


//namespace Newbee.BLL.DTO.Auth.Responses
//{
//    public record AuthResponse
//    (
//        int  UserId,
//        string Email,
//        string FirstName,
//        string LastName,
//        string Token,
//        int ExpiresIn
//        );
    
//}
