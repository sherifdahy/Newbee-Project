using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ShoppingCartItem 
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public DateTime AddedAt { get; set; }

    public int ShoppingCartId { get;set; }
    public virtual ShoppingCart ShoppingCart { get; set; }

    public int ProductUnitId { get; set; }
    public virtual ProductUnit ProductUnit { get; set; }
}
