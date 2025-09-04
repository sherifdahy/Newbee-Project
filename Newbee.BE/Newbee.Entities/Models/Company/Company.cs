using Newbee.Entities.Models;

namespace Newbee.Entities;

public class Company : TrackingBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxRegistrationNumber { get; set; } = string.Empty;
    public Guid ApiKey { get; set; }
    public ICollection<Platform> Platforms { get; set; } = new HashSet<Platform>();
    public ICollection<District> Districts { get; set; } = new HashSet<District>();
    public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
    
}
