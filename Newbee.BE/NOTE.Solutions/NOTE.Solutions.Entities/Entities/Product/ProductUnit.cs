using NOTE.Solutions.Entities.Entities.Order;

namespace NOTE.Solutions.Entities.Entities.Product;
public class ProductUnit : AuditableEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string InternalBarcode { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int UnitId { get; set; }
    public int ProductId { get; set; }


    public Unit.Unit Unit { get; set; } = default!;
    public Product Product { get; set; } = default!;
    public GlobalBarcode GlobalBarcode { get; set; } = default!;
    public ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
}


