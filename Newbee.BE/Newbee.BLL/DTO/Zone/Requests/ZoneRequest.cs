using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Zone.Requests
{
    public class ZoneRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CityId { get; set; }
    }
}
