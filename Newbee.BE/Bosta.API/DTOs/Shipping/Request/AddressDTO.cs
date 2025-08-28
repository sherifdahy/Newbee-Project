using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Request
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string ZoneId { get; set; }
        public string DistrictId { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string BuildingNumber { get; set; }
        public string Floor { get; set; }
        public string Apartment { get; set; }
    }

}
