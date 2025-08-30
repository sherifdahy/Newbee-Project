using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bosta.API.DTOs.Price
{
    public class PricingRequestDTO
    {
        public decimal Cod { get; set; }
        [Required]
        public string? DropOffCity { get; set; }
        [Required]
        public string? PickupCity { get; set; }
        public string? Size { get; set; }
        public string? Type { get; set; }
    }
}
