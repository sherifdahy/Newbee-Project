using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.Models;
public class Payment : TrackingBase
{
    public int Id { get; set; }
    public PaymentType Type { get; set; }
    public decimal Amount { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}

