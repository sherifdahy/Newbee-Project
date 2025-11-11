using NOTE.Solutions.Entities.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities;
public class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public int CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; }


    // should be nullable 
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedById { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
