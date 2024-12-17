namespace Restful.Core.Base
{
    public interface ICacheManager
    {
        void AddToCache<TValue>(string key, TValue value, int expiration);
        object? GetFromCache(string key);
    }
}