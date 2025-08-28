using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class RepeatedDataDTO
    {
        public string RepeatedType { get; set; }
        public List<string> Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
