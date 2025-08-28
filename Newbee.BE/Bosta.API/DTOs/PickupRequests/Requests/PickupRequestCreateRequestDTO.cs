using Api.Bosta.DTOs.PickupLocations.Shared;
using Api.Bosta.DTOs.PickupRequests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Requests
{
    public class PickupRequestCreateRequestDTO
    {
        public string BusinessLocationId { get; set; }
        public string Notes { get; set; }
        public DateTime ScheduledDate { get; set; }
        public PickupLocationContactPersonDTO ContactPerson { get; set; }
        public RepeatedDataDTO RepeatedData { get; set; }
    }

}
