using Microsoft.AspNetCore.Http;

namespace NOTE.Solutions.BLL.Errors;

public static class GovernateErrors
{
    public static readonly Error Duplicated = new("Governate.Duplicated", "The governate already exists.", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidId = new("Governate.InvalidId", "The provided governate ID is invalid.", StatusCodes.Status400BadRequest);
    public static readonly Error NotFound = new("Governate.NotFound", "The governate was not found.", StatusCodes.Status404NotFound);
}