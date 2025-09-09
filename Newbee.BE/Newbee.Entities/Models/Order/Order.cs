<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
﻿using Newbee.Entities.Models;

>>>>>>> d8396f1e8a19c352a3f4ce797f9913b100ef5c2b

namespace Newbee.Entities;
public class Order : TrackingBase
{
    public int Id { get; set; }
    public OrderState State { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
<<<<<<< HEAD

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }


=======
    public string PaymobOrderId { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public virtual Payment Payment { get; set; }
>>>>>>> d8396f1e8a19c352a3f4ce797f9913b100ef5c2b
    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();

}



