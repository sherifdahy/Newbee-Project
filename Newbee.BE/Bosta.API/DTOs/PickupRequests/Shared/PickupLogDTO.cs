using Api.Bosta.DTOs.Shipping.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class PickupLogDTO
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string ActionType { get; set; }
        public DateTime Time { get; set; }
        public TakenByDTO TakenBy { get; set; }
    }
}
