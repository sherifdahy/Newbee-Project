using NOTE.Solutions.Entities.Entities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Address;
public class City 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int GovernorateId { get; set; }
    public Governorate Governorate { get; set; } = default!;

    public ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
}
