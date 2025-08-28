using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class ShipmentResponseDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public ShipmentDataDTO data { get; set; }
    }
}

