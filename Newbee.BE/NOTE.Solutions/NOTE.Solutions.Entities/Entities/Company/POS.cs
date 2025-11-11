using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Company;

public class POS : AuditableEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string POSSerial { get; set; } = string.Empty;

    public int BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}
