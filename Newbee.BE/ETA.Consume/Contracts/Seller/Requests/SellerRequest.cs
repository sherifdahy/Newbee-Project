
namespace ETA.Consume.Contracts.Seller.Requests;

public class SellerRequest
{
    public string RIN { get; set; } = string.Empty;
    public string CompanyTradeName { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string DeviceSerialNumber { get; set; } = string.Empty;
    public string ActivityCode { get; set; } = string.Empty;
    public BranchAddressRequest BranchAddress { get; set; }
}
