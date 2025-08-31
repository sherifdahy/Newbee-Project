using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class TrackingResponseDTO
    {
        public string DeliveryId { get; set; }
        public string TrackingNumber { get; set; }
        public string CurrentState { get; set; }
        public List<TrackingEventDTO> Events { get; set; }
    }
}
