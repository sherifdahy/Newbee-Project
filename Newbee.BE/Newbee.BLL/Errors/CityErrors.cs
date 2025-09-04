using Microsoft.AspNetCore.Http;

namespace Newbee.BLL.Errors
{
    public static class CityErrors
    {
        public static readonly Error NotFound =
            new Error("City.NotFound", "City does not exist.", StatusCodes.Status404NotFound);

        public static readonly Error InvalidId =
            new Error("City.InvalidId", "Invalid City ID.", StatusCodes.Status400BadRequest);

        public static readonly Error DuplicatedCity =
            new Error("City.DuplicatedCity", "City already exists.", StatusCodes.Status409Conflict);

        public static readonly Error CountryNotFound =
            new Error("City.CountryNotFound", "Associated country does not exist.", StatusCodes.Status404NotFound);
    }
}
