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

<<<<<<< HEAD:Newbee.BE/Newbee.BLL/DTO/Requests/AuthResponse.cs
=======
namespace Newbee.BLL.DTO.Auth.Responses
{
    public record AuthResponse
    (
        int  UserId,
        string Email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresIn
        );
    
}
>>>>>>> a8b24b0dda37bb7dd90680706e9d425acc166678:Newbee.BE/Newbee.BLL/DTO/Auth/Responses/AuthResponse.cs
