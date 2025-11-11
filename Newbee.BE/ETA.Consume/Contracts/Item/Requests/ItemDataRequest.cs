namespace ETA.Consume.Contracts.Item.Requests;

public class ItemRequest
{
    public string InternalCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public decimal Quantity { get; set; } 
    public decimal UnitPrice { get; set; } 
    public decimal NetSale { get; set; } 
    public decimal TotalSale { get; set; } 
    public decimal Total { get; set; } 

}
