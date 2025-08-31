using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class ReceiverDTO
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string secondPhone { get; set; }
    }
}
