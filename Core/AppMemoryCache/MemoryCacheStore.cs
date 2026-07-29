using Microsoft.Extensions.Caching.Memory;

namespace AISample.Core.AppMemoryCache
{
    public class MemoryCacheStore : IMemoryCacheStore
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheStore(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public T GetOrCreate<T>(string key) where T : new()
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            return _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return new T();
            });
        }

        public T GetOrCreate<T>(string key, Func<ICacheEntry, T> factory)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            return _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                return factory(entry);
            });
        }

        public void Remove(string key)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            _memoryCache.Remove(key);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
