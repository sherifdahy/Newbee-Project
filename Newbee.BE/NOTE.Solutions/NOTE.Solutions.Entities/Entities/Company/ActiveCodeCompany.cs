
namespace NOTE.Solutions.Entities.Entities.Company;

public class ActiveCodeCompany
{
    public int ActiveCodeId { get; set; }
    public int CompanyId { get; set; }

    public ActiveCode ActiveCode { get; set; } = default!;
    public Company Company { get; set; } = default!;
}
