using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Request
{
    public class SpecsDTO
    {
        public string PackageType { get; set; }
        public string Size { get; set; }
        public PackageDetailsDTO PackageDetails { get; set; }
    }
}
