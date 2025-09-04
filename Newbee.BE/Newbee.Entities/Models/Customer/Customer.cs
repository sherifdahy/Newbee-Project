
using Newbee.Entities.Models;

namespace Newbee.Entities;
public class Customer : TrackingBase
{
    public int Id { get; set; }
    public string FirstLine { get; set; } = string.Empty;

    public int ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    public ICollection<District> Districts { get; set; } = new HashSet<District>();

}
