using NOTE.Solutions.BLL.Contracts.Manager.Requests;

namespace NOTE.Solutions.BLL.Contracts.Company.Requests;

public class CreateCompanyRequest
{
    public string Name { get; set; } = string.Empty;
    public string RIN { get; set; } = string.Empty;
}
