using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class TimelineItemDTO
    {
        public string value { get; set; }
        public int code { get; set; }
        public bool done { get; set; }
        public string date { get; set; }
    }
}
