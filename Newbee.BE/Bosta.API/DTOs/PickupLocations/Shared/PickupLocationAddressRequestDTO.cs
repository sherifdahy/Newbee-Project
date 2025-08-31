using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Shared
{
    public class PickupLocationAddressRequestDTO
    {
        public string DistrictId { get; set; }

        public string FirstLine { get; set; }
    }
}
