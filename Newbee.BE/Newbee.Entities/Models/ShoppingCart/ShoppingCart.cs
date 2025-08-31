using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ShoppingCart : TrackingBase
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }


    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
}
