using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class CreateShipmentDataDTO
    {
        public string _id { get; set; }
        public string TrackingNumber { get; set; }
        public string BusinessReference { get; set; }
        public SenderDTO Sender { get; set; }
        public string Message { get; set; }
        public StateDTO State { get; set; }
        public string CreationSrc { get; set; }
    }
}
