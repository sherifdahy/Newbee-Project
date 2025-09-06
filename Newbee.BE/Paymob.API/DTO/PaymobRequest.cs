using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymob.API.DTO
{
    public class PaymobRequest
    {
        public int OrderId { get; set; }
        public decimal AmountCents { get; set; }         public string Currency { get; set; } = "EGP";
        public BillingDataRequest BillingData { get; set; }
    }
}
