using Newtonsoft.Json;

namespace ETA.Consume.Interfaces;

public interface IUUIDService
{
    public string GenerateUUID<T>(T document);
}
