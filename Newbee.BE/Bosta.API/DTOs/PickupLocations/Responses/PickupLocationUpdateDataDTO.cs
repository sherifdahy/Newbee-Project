using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Responses
{
    public class PickupLocationUpdateDataDTO
    {
        public string LocationId { get; set; }
        public bool IsDefault { get; set; }
    }

}
