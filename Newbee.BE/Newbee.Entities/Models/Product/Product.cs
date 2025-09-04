using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class Product : TrackingBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public ICollection<ProductUnit> ProductUnits { get; set; } = new HashSet<ProductUnit>();
}
