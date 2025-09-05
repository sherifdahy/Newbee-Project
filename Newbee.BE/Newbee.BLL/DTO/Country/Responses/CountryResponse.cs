using Newbee.BLL.DTO.Country.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Country.Responses
{
    public class CountryResponse : CountryRequest
    {
        public int Id { get; set; }
    }
}
