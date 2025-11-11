using NOTE.Solutions.BLL.Contracts.Customer.Responses;
using NOTE.Solutions.BLL.Contracts.DocumentType.Responses;
using NOTE.Solutions.BLL.Contracts.OrderLine.Responses;
using NOTE.Solutions.BLL.Contracts.POS.Responses;
using NOTE.Solutions.Entities.Enums;


namespace NOTE.Solutions.BLL.Contracts.Document.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public DocumentTypeResponse DocumentType { get; set; } = default!;
    public List<OrderLineResponse> OrderLines { get; set; } = [];
    public DocumentHeaderResponse Header { get; set; } = default!;
    public CustomerResponse Customer { get; set; } = default!;
    public PointOfSaleResponse POS { get; set; } = default!;
    public PaymentMethodType PaymentMethod { get; set; }

}
