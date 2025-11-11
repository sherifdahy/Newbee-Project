using NOTE.Solutions.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Manager;
public class Manager 
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int ApplicationUserId { get; set; }
    public int CompanyId { get; set; }

    public Company.Company Company { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;
}
