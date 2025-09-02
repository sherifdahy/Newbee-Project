using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class Order : TrackingBase
{
    public int Id { get; set; }
    public OrderState State { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }


    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();

}



