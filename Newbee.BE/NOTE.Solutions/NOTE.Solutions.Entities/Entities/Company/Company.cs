namespace NOTE.Solutions.Entities.Entities.Company;
public class Company 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RIN { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public ICollection<Manager.Manager> Managers { get; set; } = new HashSet<Manager.Manager>();
    public ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    public ICollection<ActiveCodeCompany> ActiveCodeCompanies { get; set; } = new HashSet<ActiveCodeCompany>();
}
