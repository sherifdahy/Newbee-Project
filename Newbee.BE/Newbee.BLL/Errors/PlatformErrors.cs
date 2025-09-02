using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Errors;
public class PlatformErrors
{
    public static readonly Error NotFound
        = new Error("Platform.NotFound", "Platform is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Platform.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);

}
