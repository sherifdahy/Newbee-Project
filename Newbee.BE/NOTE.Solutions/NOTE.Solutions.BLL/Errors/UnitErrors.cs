using Microsoft.AspNetCore.Http;

namespace NOTE.Solutions.BLL.Errors;

public static class UnitErrors
{
    public static readonly Error NotFound
        = new Error("Unit.NotFound", "Unit is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Unit.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("Unit.Duplicated", "Unit is Already Exist.", StatusCodes.Status409Conflict);
}
