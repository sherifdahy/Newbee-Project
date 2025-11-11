using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Errors;

public static class POSErrors
{
    public static readonly Error NotFound
        = new Error("POS.NotFound", "POS is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("POS.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("POS.Duplicated", "This POS is Already Exist.", StatusCodes.Status409Conflict);
}
