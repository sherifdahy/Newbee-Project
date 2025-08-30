using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Common.Errors
{
    public class ErrorsCatalog
    {
        public static class Auth
        {
            public static Error InvalidId => new Error("", "");
        }
    }
}
