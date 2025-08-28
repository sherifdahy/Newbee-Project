using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class SenderDTO
    {
        public string _id { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string subAccountId { get; set; }
    }

}
