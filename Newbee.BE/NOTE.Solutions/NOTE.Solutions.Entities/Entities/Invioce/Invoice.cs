using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Invioce;

public class Invoice : AuditableEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order.Order Order { get; set; } = default!;
}
