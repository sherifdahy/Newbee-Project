using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Bosta.DTOs.Shipping.Response
{
    public class WalletDTO
    {
        public object cashCycle { get; set; }
        public CashoutDTO cashout { get; set; }
        public object compensation { get; set; }
    }
}
