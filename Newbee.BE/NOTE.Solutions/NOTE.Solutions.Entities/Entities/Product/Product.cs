using NOTE.Solutions.Entities.Entities.Category;
using NOTE.Solutions.Entities.Entities.Company;

namespace NOTE.Solutions.Entities.Entities.Product;
public class Product : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
   public int CategoryId { get; set; }
    public Category.Category Category { get; set; } = default!;

    public ICollection<ProductUnit> ProductUnits { get; set; } = new HashSet<ProductUnit>();
}
