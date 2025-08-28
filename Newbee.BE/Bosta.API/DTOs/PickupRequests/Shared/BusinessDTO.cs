using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class BusinessDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public BusinessAddressDTO Address { get; set; }
        public string LocationName { get; set; }
        public bool NewBusiness { get; set; }
        public string Phone { get; set; }
    }
}
