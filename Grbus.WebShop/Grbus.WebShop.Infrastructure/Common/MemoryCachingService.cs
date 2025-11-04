using Grbus.WebShop.Application.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class MemoryCachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task Remove(string key)
        {
            _memoryCache.Remove(key);
            await Task.CompletedTask;
        }

        public async Task Set<T>(string key, T value)
        {
            _memoryCache.Set(key, value);
            await Task.CompletedTask;
        }

        public bool TryGet<T>(string key, out T? value)
        {
            _memoryCache.TryGetValue(key, out value);
            return value != null;
        }
    }
}
