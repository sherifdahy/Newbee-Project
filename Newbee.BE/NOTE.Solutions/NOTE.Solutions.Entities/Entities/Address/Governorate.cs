using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Address;
public class Governorate 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int CountryId { get; set; }
    public Country Country { get; set; } = default!;

    public ICollection<City> Cities{ get; set; }= new HashSet<City>();
    
}
