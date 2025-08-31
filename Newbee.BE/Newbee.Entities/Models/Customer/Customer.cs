using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class Customer : TrackingBase
{
    public int Id { get; set; }
    public string FirstLine { get; set; }

    public int ApplicationUserId { get; set; }
    public virtual ApplicationUser Application { get; set; }

    public  int DistrictId { get; set; }
    public virtual District District { get; set; }


    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
