using NOTE.Solutions.BLL.Contracts.Governate.Responses;

namespace NOTE.Solutions.BLL.Contracts.City.Responses;

public class CityResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public GovernateResponse Governorate { get; set; } = default!;
}
