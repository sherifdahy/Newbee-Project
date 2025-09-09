using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Users
{
    public record UpdateProfileRequest
    (
        string FirstName,
        string LastName,
        string PhoneNumber,
        string FirstLine
    );
    
}
