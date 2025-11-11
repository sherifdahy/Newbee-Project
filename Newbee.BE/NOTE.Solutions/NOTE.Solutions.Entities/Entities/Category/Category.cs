using NOTE.Solutions.Entities.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Category;

public class Category : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted{ get; set; }= false;
    public int BranchId { get; set; }
    public Branch Branch { get; set; }
    public ICollection<Product.Product> Products { get; set; } = new HashSet<Product.Product>();
}
