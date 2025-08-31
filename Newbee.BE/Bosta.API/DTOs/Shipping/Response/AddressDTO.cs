using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class AddressDTO
    {
        public CountryDTO country { get; set; }
        public CityDTO city { get; set; }
        public ZoneDTO zone { get; set; }
        public DistrictDTO district { get; set; }
        public string firstLine { get; set; }
        public string secondLine { get; set; }
        public string buildingNumber { get; set; }
        public string floor { get; set; }
        public string apartment { get; set; }
        public string originalFirstLine { get; set; }
        public string originalSecondLine { get; set; }
    }

}
