
using Newbee.Entities.Models;
using Newbee.Entities.Models.Platform;

namespace Newbee.Entities
{
    public class Company : TrackingBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TaxRegistrationNumber { get; set; } = string.Empty;

        public ICollection<Platform> Platforms { get; set; } = new HashSet<Platform>();
        public ICollection<District> Districts { get; set; } = new HashSet<District>();
        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
        
    }
}
