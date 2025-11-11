using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Receipt;

public class Receipt : AuditableEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order.Order Order { get; set; } = default!;

}
