using NOTE.Solutions.Entities.Enums;

namespace NOTE.Solutions.BLL.Contracts.ProductUnit.Requests;

public class ProductUnitRequest
{
    public string Description { get; set; } = string.Empty;
    public string InternalCode { get; set; } = string.Empty;
    public string GlobalCode { get; set; } = string.Empty;
    public GlobalCodeType GlobalCodeType { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitId { get; set; }
}
