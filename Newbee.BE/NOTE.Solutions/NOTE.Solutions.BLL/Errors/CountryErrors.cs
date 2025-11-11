using Microsoft.AspNetCore.Http;

namespace NOTE.Solutions.BLL.Errors;

public class CountryErrors
{
    public static readonly Error NotFound
        = new Error("Country.NotFound", "Country is Not Exist.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidId
        = new Error("Country.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
    public static readonly Error Duplicated =
            new("Country.Duplicated", "Country is Already Exist.", StatusCodes.Status409Conflict);
}
