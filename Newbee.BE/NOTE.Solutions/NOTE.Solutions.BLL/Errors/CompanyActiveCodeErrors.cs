using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Errors;

public static class CompanyActiveCodeErrors
{
    public static readonly Error NotFound = new(
        "CompanyActiveCode.NotFound",
        "The specified active code company association was not found.",
        StatusCodes.Status404NotFound
    );

    public static readonly Error Duplicated = new(
        "CompanyActiveCode.Duplicated",
        "The specified active code is already assigned to this company.",
        StatusCodes.Status409Conflict
    );
}