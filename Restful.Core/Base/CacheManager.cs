using Microsoft.Extensions.Caching.Memory;

namespace Restful.Core.Base
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public CacheManager()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        // Add to cache
        public void AddToCache<TValue>(string key, TValue value, int expiration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(expiration));
        }

        // Retrieve from cache
        public object? GetFromCache(string key)
        {
            return _memoryCache.TryGetValue(key, out var value) ? value : null;
        }
    }
}
