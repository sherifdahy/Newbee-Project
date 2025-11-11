namespace ETA.Consume.Contracts.Customer.Requests;

public class CustomerRequest
{
    public string Type { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; } 
    public string? MobileNumber { get; set; }
}
