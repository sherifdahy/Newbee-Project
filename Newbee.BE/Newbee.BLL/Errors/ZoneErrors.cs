using Microsoft.AspNetCore.Http;

namespace Newbee.BLL.Errors
{
    public static class ZoneErrors
    {
        public static readonly Error NotFound =
            new Error("Zone.NotFound", "Zone does not exist.", StatusCodes.Status404NotFound);

        public static readonly Error InvalidId =
            new Error("Zone.InvalidId", "Invalid Zone ID.", StatusCodes.Status400BadRequest);

        public static readonly Error DuplicatedZone =
            new Error("Zone.DuplicatedZone", "Zone already exists.", StatusCodes.Status409Conflict);

        public static readonly Error CityNotFound =
            new Error("Zone.CityNotFound", "Associated city does not exist.", StatusCodes.Status404NotFound);
    }
}
