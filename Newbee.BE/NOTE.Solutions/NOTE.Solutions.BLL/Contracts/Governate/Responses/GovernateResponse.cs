using NOTE.Solutions.BLL.Contracts.Country.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.Governate.Responses;

public class GovernateResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public CountryResponse Country { get; set; } = default!;
}
