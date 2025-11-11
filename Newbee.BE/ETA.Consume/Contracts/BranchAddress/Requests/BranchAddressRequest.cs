namespace ETA.Consume.Contracts.BranchAddress.Requests;

public class BranchAddressRequest
{
    public string Country { get; set; } = string.Empty;
    public string Governate { get; set; } = string.Empty;
    public string RegionCity { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
}
