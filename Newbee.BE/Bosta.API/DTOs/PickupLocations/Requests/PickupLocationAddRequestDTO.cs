using Api.Bosta.DTOs.PickupLocations.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Responses
{
    public class PickupLocationAddRequestDTO
    {
        public PickupLocationAddressRequestDTO Address { get; set; }

        public List<PickupLocationContactRequestDTO> Contacts { get; set; }

        public string LocationName { get; set; }
    }
}
