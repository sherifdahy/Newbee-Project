
namespace NOTE.Solutions.BLL.Contracts.OrderLine.Requests;

public class OrderLineRequest
{
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public int ProductUnitId { get; set; }

}
