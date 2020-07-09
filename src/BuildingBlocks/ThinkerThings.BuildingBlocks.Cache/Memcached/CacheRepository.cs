namespace ThinkerThings.BuildingBlocks.Cache.Memcached
{
    using Enyim.Caching;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public interface ICacheRepository
    {
        Task Set<T>(string key, T value);
    }

    public class CacheRepository : ICacheRepository
    {
        private readonly ILogger<CacheRepository> _logger;
        private readonly IMemcachedClient _memcachedClient;

        public CacheRepository(ILogger<CacheRepository> logger, IMemcachedClient memcachedClient)
        {
            _logger = logger;
            _memcachedClient = memcachedClient;
        }

        public async Task Set<T>(string key, T value)
        {
            try
            {
                _logger.LogDebug($"Removendo item do cache com a chave: {key}");
                await _memcachedClient.RemoveAsync(key);

                _logger.LogDebug($"Registrando item do cache com a chave: {key}");
                await _memcachedClient.SetAsync(key, value, 60 * 60);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Falha ao registrar item no cache com a chave: {ex}");
            }
        }
    }
}