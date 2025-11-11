
using NOTE.Solutions.BLL.Contracts.Employee.Requests;
using NOTE.Solutions.BLL.Contracts.POS.Requests;

namespace NOTE.Solutions.BLL.Contracts.Branch.Requests;

public class BranchRequest
{
    public string Code { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int CityId { get; set; }
}
