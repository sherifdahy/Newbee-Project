using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class OrderDetails
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }


    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    public int ProductUnitId { get; set; }
    public virtual ProductUnit ProductUnit { get; set; }
}
