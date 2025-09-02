using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ProductCategory : TrackingBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
