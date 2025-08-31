using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Request
{
    public class CreateShipmentRequestDTO
    {
        public int Type { get; set; }
        public SpecsDTO Specs { get; set; }
        public string Notes { get; set; }
        public decimal Cod { get; set; }
        public AddressDTO DropOffAddress { get; set; }
        public AddressDTO PickupAddress { get; set; }
        public AddressDTO ReturnAddress { get; set; }
        public string BusinessReference { get; set; }
        public ReceiverDTO Receiver { get; set; }
        public string WebhookUrl { get; set; }
    }
    
}
