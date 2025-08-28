using Api.Bosta.DTOs.PickupLocations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Requests
{
    public class PickupLocationUpdateRequestDTO
    {
        public string LocationName { get; set; }
        public PickupLocationContactPersonDTO ContactPerson { get; set; }
        public PickupLocationAddressRequestDTO Address { get; set; }
    }
}
