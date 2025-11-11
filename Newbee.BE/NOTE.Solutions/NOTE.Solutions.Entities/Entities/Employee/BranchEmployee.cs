using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Employee;

public class BranchEmployee : AuditableEntity
{
    public int BranchId { get; set; }
    public int EmployeeId { get; set; }
    public Branch Branch { get; set; } = default!;
    public Employee Employee { get; set; } = default!;
}
