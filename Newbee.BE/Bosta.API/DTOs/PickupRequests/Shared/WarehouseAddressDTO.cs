using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class WarehouseAddressDTO
    {
        public string FirstLine { get; set; }
        public CityDTO City { get; set; }
        public ZoneDTO Zone { get; set; }
        public DistrictDTO District { get; set; }
    }
}
