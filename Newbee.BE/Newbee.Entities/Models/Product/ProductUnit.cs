using Newbee.Entities.Models.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ProductUnit : TrackingBase
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Stock { get; set; }
    public decimal Price { get; set; }
    public decimal Rate { get; set; }

    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int ProductColorId { get; set; }
    public virtual ProductColor  ProductColor { get; set; }

    public int ProductSizeId { get; set; }
    public virtual ProductSize ProductSize { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();

}
