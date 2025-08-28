using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Request
{
    public class PackageDetailsDTO
    {
        public int ItemsCount { get; set; }
        public string Description { get; set; }
    }
}
