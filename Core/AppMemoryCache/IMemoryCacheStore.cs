using Microsoft.Extensions.Caching.Memory;

namespace AISample.Core.AppMemoryCache
{
    public interface IMemoryCacheStore
    {
        T GetOrCreate<T>(string key) where T : new();
        T GetOrCreate<T>(string key, Func<ICacheEntry, T> factory);
        void Remove(string key);
        bool TryGetValue<T>(string key, out T? value);
    }
}