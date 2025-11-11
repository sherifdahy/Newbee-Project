using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Errors;

public static class CompanyErrors
{
    public static readonly Error NotFound
        = new Error("Company.NotFound", "Company is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Company.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("Company.DuplicatedRIN", "RIN is Already Exist.", StatusCodes.Status409Conflict);
}
