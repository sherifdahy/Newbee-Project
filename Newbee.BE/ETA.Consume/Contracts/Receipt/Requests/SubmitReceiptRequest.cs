using ETA.Consume.Contracts.Customer.Requests;
using ETA.Consume.Contracts.Header.Requests;

namespace ETA.Consume.Contracts.Receipt.Requests;

public class SubmitReceiptRequest
{
    public HeaderRequest Header { get; set; } = default!;
    public DocumentTypeRequest DocumentType { get; set; } = default!;
    public SellerRequest Seller { get; set; } = default!;
    public CustomerRequest Buyer { get; set; } = default!;
    public List<ItemRequest> ItemData { get; set; } = [];
    public decimal TotalSales { get; set; }
    public decimal NetAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}
