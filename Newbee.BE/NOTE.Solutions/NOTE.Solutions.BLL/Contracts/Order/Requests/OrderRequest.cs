using NOTE.Solutions.BLL.Contracts.Customer.Requests;
using NOTE.Solutions.BLL.Contracts.OrderLine.Requests;
using NOTE.Solutions.Entities.Entities.Order;
using NOTE.Solutions.Entities.Enums;

namespace NOTE.Solutions.BLL.Contracts.Document.Requests;

public class OrderRequest 
{
    public DocumentHeaderRequest Header { get; set; } = default!;
    public CustomerRequest Customer { get; set; } = default!;
    public PaymentMethodType PaymentMethod { get; set; }
    public int ActiveCodeId { get; set; }
    public int PosId { get; set; }
    public int DocumentTypeId { get; set; }
    public List<OrderLineRequest> OrderLines { get; set; } = [];
}
