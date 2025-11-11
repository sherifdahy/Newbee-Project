using NOTE.Solutions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.Entities.Entities.Product;

public class GlobalBarcode : AuditableEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public GlobalCodeType Type { get; set; }

    public int ProductUnitId { get; set; }
    public ProductUnit ProductUnit { get; set; } = default!;
}
