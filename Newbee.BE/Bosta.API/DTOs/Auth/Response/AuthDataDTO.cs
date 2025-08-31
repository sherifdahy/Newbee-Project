using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Auth.Response
{
    public class AuthDataDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public AuthUserDTO User { get; set; }
    }
}
