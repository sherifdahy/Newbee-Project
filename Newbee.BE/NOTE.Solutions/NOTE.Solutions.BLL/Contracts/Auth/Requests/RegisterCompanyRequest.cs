using NOTE.Solutions.BLL.Contracts.Manager.Requests;

namespace NOTE.Solutions.BLL.Contracts.Auth.Requests;

public class RegisterCompanyRequest
{
    // company information
    public string Name { get; set; } = string.Empty;
    public string RIN { get; set; } = string.Empty;
    public ManagerRequest Manager { get; set; } = default!;
}
