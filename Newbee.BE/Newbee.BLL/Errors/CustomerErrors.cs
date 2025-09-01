using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Errors;
public class CustomerErrors
{
    public static readonly Error NotFound
        = new Error("Customer.NotFound", "Customer is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Customer.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
}
