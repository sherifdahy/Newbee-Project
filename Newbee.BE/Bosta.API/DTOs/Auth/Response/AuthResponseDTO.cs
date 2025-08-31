using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Auth.Response
{
    public class AuthResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AuthDataDTO Data { get; set; }
    }
}



