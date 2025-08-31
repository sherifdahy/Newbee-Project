using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class CreateShipmentResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public CreateShipmentDataDTO Data { get; set; }
    }

    

}
