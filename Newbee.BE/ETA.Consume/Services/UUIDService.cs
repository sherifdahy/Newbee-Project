using ETA.Consume.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ETA.Consume.Services;

public class UUIDService : IUUIDService
{
    public string GenerateUUID<T>(T document)
    {
        return Hash(serialize_text(JsonConvert.SerializeObject(document, JsonSettingsSerializer.Options)));
    }
    private string Hash(string input)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            string hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hexString;
        }
    }
    private string serialize_text(string json)
    {
        JObject request = JsonConvert.DeserializeObject<JObject>(json,JsonSettingsSerializer.Options)!;

        String canonicalString = SerializeJSONToken(request);
        return canonicalString;
    }
    private string SerializeJSONToken(JToken request)
    {
        string serialized = "";
        if (request.Parent is null)
        {
            SerializeJSONToken(request.First!);
        }
        else
        {
            if (request.Type == JTokenType.Property)
            {
                string name = ((JProperty)request).Name.ToUpper();
                serialized += "\"" + name + "\"";
                foreach (var property in request)
                {
                    if (property.Type == JTokenType.Object)
                    {
                        serialized += SerializeJSONToken(property);
                    }
                    if (property.Type == JTokenType.Boolean || property.Type == JTokenType.Integer || property.Type == JTokenType.Float || property.Type == JTokenType.Date)
                    {
                        serialized += "\"" + property.Value<string>() + "\"";
                    }
                    if (property.Type == JTokenType.String)
                    {
                        serialized += JsonConvert.ToString(property.Value<string>());
                    }
                    if (property.Type == JTokenType.Array)
                    {
                        foreach (var item in property.Children())
                        {
                            serialized += "\"" + ((JProperty)request).Name.ToUpper() + "\"";
                            serialized += SerializeJSONToken(item);
                        }
                    }
                }
            }
            if (request.Type == JTokenType.String)
            {
                serialized += JsonConvert.ToString(request.Value<string>());
            }
        }
        if (request.Type == JTokenType.Object)
        {
            foreach (var property in request.Children())
            {

                if (property.Type == JTokenType.Object || property.Type == JTokenType.Property)
                {
                    serialized += SerializeJSONToken(property);
                }
            }
        }

        return serialized;
    }

}
