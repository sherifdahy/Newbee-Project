using NOTE.Solutions.BLL.Contracts.Manager.Responses;
using NOTE.Solutions.Entities.Entities.Company;

namespace NOTE.Solutions.BLL.Contracts.Company.Responses;

public class CompanyResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RIN { get; set; } = string.Empty;
    public List<ActiveCodeResponse> ActiveCodes { get; set; } = [];
    public List<BranchResponse> Branches { get; set; } = default!;
    public List<ManagerResponse> Managers { get; set; } = [];
}
