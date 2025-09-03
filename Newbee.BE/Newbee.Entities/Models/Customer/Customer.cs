
namespace Newbee.Entities;
public class Customer : TrackingBase
{
    public int Id { get; set; }
    public string FirstLine { get; set; } = string.Empty;

    public int ApplicationUserId { get; set; }
    public virtual ApplicationUser Application { get; set; }

    public  int DistrictId { get; set; }
    public virtual District District { get; set; }


    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
