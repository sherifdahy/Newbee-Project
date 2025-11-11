using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Order;
public class Order : AuditableEntity
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public int CustomerId { get; set; }
    public int ActiveCodeId { get; set; }
    public PaymentMethodType PaymentMethod { get; set; }


    public ActiveCode ActiveCode { get; set; } = default!;
    public Branch Branch { get; set; } = default!;
    public Customer.Customer Customer { get; set; } = default!;
    public ICollection<OrderLine> OrderDetails { get; set; } = new HashSet<OrderLine>();
}



