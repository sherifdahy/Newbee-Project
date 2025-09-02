using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ProductSize : TrackingBase
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}
