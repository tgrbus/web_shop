using Grbus.WebShop.Application.Common;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class RedisCachingService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCachingService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Remove(string key)
        {
            throw new NotImplementedException();
        }

        public async Task Set<T>(string key, T value)
        {
            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value));
        }

        public bool TryGet<T>(string key, out T? value)
        {
            var cachedData = _distributedCache.GetString(key);
            if (cachedData != null)
            {
                value = JsonSerializer.Deserialize<T>(cachedData);
                return true;
            }
            value = default;
            return false;
        }
    }
}
