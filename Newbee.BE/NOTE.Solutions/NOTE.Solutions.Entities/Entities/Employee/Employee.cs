using NOTE.Solutions.Entities.Entities.Company;
using NOTE.Solutions.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Employee;
public class Employee : AuditableEntity
{
    public int Id { get; set; }
    public int ApplicationUserId { get; set; }
    public bool IsDeleted { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public ICollection<BranchEmployee> BranchEmplyees { get; set; } = new HashSet<BranchEmployee>();
}
