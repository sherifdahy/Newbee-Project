using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.City.Requests
{
    public class CityRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
    }
}
