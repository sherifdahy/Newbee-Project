using Api.Bosta.DTOs.Shipping.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class BusinessAddressDTO
    {
        public CityDTO City { get; set; }
        public ZoneDTO Zone { get; set; }
        public DistrictDTO District { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
        public string BuildingNumber { get; set; }
        public bool IsWorkAddress { get; set; }
    }
}
