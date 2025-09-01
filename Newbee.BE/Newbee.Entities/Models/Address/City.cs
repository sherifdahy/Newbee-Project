﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Entities;
public class City
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }
}
