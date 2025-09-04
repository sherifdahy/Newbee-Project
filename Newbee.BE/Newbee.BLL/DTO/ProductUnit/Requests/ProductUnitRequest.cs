using Newbee.Entities.Models.Unit;

namespace Newbee.BLL.DTO.ProductUnit.Requests;
public class ProductUnitRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Stock { get; set; }
    public decimal Price { get; set; }
    public decimal Rate { get; set; }
    public int UnitId { get; set; }
    public int ProductId { get; set; }
    public int ProductColorId { get; set; }
    public int ProductSizeId { get; set; }
}
