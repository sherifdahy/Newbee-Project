using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
