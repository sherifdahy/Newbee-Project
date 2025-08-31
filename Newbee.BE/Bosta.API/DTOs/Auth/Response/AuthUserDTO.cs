using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Auth.Response
{
    public class AuthUserDTO
    {
        public string Id { get; set; }
        public List<AuthEmailDTO> Emails { get; set; }
        public AuthProfileDTO Profile { get; set; }
        public List<string> Roles { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsFirstOrderCreated { get; set; }
        public BusinessAdminInfoDTO BusinessAdminInfo { get; set; }
    }
}
