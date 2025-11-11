using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Employee.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string IdentifierNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
