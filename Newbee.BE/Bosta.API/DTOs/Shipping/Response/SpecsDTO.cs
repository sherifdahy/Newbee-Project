using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class SpecsDTO
    {
        public string packageType { get; set; }
        public PackageDetailsDTO packageDetails { get; set; }
        public double weight { get; set; }
    }
}
