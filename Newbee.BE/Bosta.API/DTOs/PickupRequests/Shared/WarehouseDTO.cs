using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.PickupRequests.Shared
{
    public class WarehouseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public WarehouseAddressDTO Address { get; set; }
    }
}
