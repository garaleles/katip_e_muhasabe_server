using eMuhasebeServer.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace eMuhasebeServer.Infrastructure.Services;

public sealed class MemoryCacheService(IMemoryCache cache) : ICacheService
{
    public T? Get<T>(string key)
    {
        cache.TryGetValue<T>(key, out var value);
        return value;
    }

    public void Set<T>(string key, T value, TimeSpan? expire = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expire ?? TimeSpan.FromHours(1)
        };

        cache.Set<T>(key, value, cacheEntryOptions);
    }

    public bool Remove(string key)
    {
        cache.Remove(key);
        return true;
    }
    
    public void RemoveAll()
    {
        List<string> keys = new()
        {
            "cacheRegitsers",
            "banks",
            "invoices",
            "products",
            "customers",
            
        };
        foreach (var key in keys)
        {
            cache.Remove(key);
        }
    }
}
