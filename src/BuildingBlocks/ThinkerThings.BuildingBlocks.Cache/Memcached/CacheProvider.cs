using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThinkerThings.BuildingBlocks.Cache.Memcached
{
    public interface ICacheProvider
    {
        ValueTask<T> Get<T>(string key);

        ValueTask<T> GetValueOrCreate<T>(string key, Func<Task<T>> func);

        ValueTask<IDictionary<string, T>> Get<T>(IEnumerable<string> keys);
    }

    public class CacheProvider : ICacheProvider
    {
        private readonly IMemcachedClient _memcachedClient;
        private readonly ICacheRepository _cacheRepository;

        public CacheProvider(IMemcachedClient memcachedClient, ICacheRepository cacheRepository)
        {
            _memcachedClient = memcachedClient;
            _cacheRepository = cacheRepository;
        }

        public async ValueTask<T> Get<T>(string key)
        {
            var valueResult = await _memcachedClient.GetAsync<T>(key);

            return valueResult.HasValue ? valueResult.Value : default;
        }

        public async ValueTask<IDictionary<string, T>> Get<T>(IEnumerable<string> keys) => await _memcachedClient.GetAsync<T>(keys);

        public async ValueTask<T> GetValueOrCreate<T>(string key, Func<Task<T>> func) => await _memcachedClient.GetValueOrCreateAsync(key, 600, func);
    }
}