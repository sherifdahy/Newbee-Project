using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupLocations.Responses
{
    public class PickupLocationListItemDTO
    {
        public int Total { get; set; }
        public List<PickupLocationItemDTO> List { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Pages { get; set; }
    }

}
