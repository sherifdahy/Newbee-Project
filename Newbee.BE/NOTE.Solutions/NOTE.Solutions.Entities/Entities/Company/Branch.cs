using Microsoft.EntityFrameworkCore;
using NOTE.Solutions.Entities.Entities.Address;
using NOTE.Solutions.Entities.Entities.Employee;
using NOTE.Solutions.Entities.Entities.Identity;

namespace NOTE.Solutions.Entities.Entities.Company;

public class Branch
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int CityId { get; set; }
    public int CompanyId { get; set; }
    public bool IsDeleted { get; set; }

    public City City { get; set; } = default!;
    public Company Company { get; set; } = default!;
    public ICollection<BranchEmployee> BranchEmplyees { get; set; } = new HashSet<BranchEmployee>();
    public ICollection<POS> PointOfSales { get; set; } = new HashSet<POS>();


}
