using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.Errors
{
    public class CountryErrors
    {
        public static readonly Error NotFound
        = new Error("Country.NotFound", "Country is Not Exist.", StatusCodes.Status404NotFound);
        public static readonly Error InvalidId
            = new Error("Country.InvalidId", "Invalid Id.", StatusCodes.Status400BadRequest);
        public static readonly Error DuplicatedCountry =
                new("Country.DuplicatedCountry", "Country is Already Exist.", StatusCodes.Status409Conflict);
    }
}
