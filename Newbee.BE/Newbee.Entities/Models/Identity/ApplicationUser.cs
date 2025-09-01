using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public virtual ICollection<OTP>? OTPs { get; set; } =  new HashSet<OTP>();

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
