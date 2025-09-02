using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class ProductUnitImage : TrackingBase
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public int ProductUnitId { get; set; }
    public virtual ProductUnit ProductUnit { get; set; }
}
