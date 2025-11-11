using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOTE.Solutions.BLL.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey,CancellationToken cancellationToken = default) where T : class;
    Task SetAsync<T>(string cacheKey, T value, TimeSpan? absoluteExpire = null, CancellationToken cancellationToken = default) where T : class;
    Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default);

}
