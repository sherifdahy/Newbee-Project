using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities.Models.Unit;
public class Unit : TrackingBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
