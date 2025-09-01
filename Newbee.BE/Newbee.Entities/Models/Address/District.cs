using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class District : TrackingBase
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; }

    public ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
}
