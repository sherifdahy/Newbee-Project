using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Authentication;

public interface IJWTProvider
{
    (string token, int expiresIn) GeneratedToken(ApplicationUser applicationUser, IEnumerable<string> applicationRoles, IEnumerable<string> permissions);
    int? ValidateToken(string token);
}
