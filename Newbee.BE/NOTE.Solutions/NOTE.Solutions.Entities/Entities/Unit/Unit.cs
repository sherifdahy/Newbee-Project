using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Product;

namespace NOTE.Solutions.Entities.Entities.Unit;
public class Unit : AuditableEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public ICollection<ProductUnit> ProductUnits { get; set; } = new HashSet<ProductUnit>();
}
