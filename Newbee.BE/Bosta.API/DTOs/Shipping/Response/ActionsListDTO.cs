using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class ActionsListDTO
    {
        public BeforeAfterDTO state_value { get; set; }
        public BeforeAfterDTO state_code { get; set; }
        public BeforeAfterDTO cod { get; set; }
    }
}
