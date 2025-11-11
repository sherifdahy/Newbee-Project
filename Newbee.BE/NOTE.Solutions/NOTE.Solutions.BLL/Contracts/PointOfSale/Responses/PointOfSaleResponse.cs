using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Contracts.POS.Responses;

public class PointOfSaleResponse
{
    public int Id { get; set; }
    public string POSSerial { get; set; } = string.Empty;
}
