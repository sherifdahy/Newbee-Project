using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace NOTE.Solutions.BLL.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default) where T : class
    {
        var result = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (string.IsNullOrEmpty(result))
            return null;

        return JsonConvert.DeserializeObject<T>(result);
    }

    public async Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(cacheKey, cancellationToken);
    }

    public async Task SetAsync<T>(string cacheKey,T value, TimeSpan? absoluteExpire = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new DistributedCacheEntryOptions();

        if (absoluteExpire.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = absoluteExpire;
        }
        else
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        }

        await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(value), options, cancellationToken);
    }
}
