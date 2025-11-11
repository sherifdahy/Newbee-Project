
namespace NOTE.Solutions.BLL.Contracts.OrderLine.Responses;

public class OrderLineResponse
{
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public ProductUnitResponse? ProductUnit { get; set; }

}
