using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class StateDTO
    {
        public int code { get; set; }
        public string value { get; set; }
        public string deliveryTime { get; set; }
    }
}
