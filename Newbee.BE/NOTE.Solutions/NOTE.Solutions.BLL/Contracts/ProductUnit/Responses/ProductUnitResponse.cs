using NOTE.Solutions.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.ProductUnit.Responses;

public class ProductUnitResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string InternalCode { get; set; } = string.Empty;
    public string GlobalCode { get; set; } = string.Empty;
    public GlobalCodeType GlobalCodeType { get; set; }
    public decimal UnitPrice { get; set; }
    public UnitResponse Unit { get; set; } = default!;
    public ProductResponse Product { get; set; } = default!;


}
