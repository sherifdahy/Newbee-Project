using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Auth.Response
{
    public class AuthEmailDTO
    {
        public bool Verified { get; set; }
        public string Address { get; set; }
    }
}
