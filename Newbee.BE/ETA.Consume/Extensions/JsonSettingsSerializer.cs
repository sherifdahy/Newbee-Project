using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETA.Consume.Extensions;

public static class JsonSettingsSerializer
{
    public static JsonSerializerSettings Options = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        DateFormatString = "yyyy-MM-ddTHH:mm:ss",

        FloatParseHandling = FloatParseHandling.Decimal,
        DateParseHandling = DateParseHandling.None
    };
}
