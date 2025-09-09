using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymob.API.DTO
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "EGP";
        public string UserId { get; set; }
    }
}
