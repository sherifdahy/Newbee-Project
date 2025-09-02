using Newbee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Authentication;

public interface IJwtProvider
{
    (string token , int expiresIn) GenerateToken(ApplicationUser user); 
    string? ValidateToken(string token);
}
