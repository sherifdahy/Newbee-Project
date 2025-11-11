using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Errors;

public static class CityErrors
{
    public static readonly Error NotFound
        = new Error("City.NotFound", "City is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("City.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("City.Duplicated", "This City is Already Exist.", StatusCodes.Status409Conflict);
}
