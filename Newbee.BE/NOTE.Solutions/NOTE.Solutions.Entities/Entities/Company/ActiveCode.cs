namespace NOTE.Solutions.Entities.Entities.Company;
public class ActiveCode 
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;

    public ICollection<ActiveCodeCompany> ActiveCodeCompanies { get; set; } = new HashSet<ActiveCodeCompany>();
}
