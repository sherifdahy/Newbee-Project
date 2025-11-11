using NOTE.Solutions.Entities.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Order;
public class OrderLine
{
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public int ProductUnitId { get; set; }
    public int OrderId { get; set; }


    public Order Order { get; set; } = default!;
    public ProductUnit ProductUnit { get; set; } = default!;
}

