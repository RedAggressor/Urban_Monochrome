using Basket.Host.Configs;
using Basket.Host.Services.Interfaces;
using StackExchange.Redis;
using Microsoft.Extensions.Options;
using Infrastucture.Services.Abstractions;

namespace Basket.Host.Services
{
    public class CacheService : ICacheService
    {
        private readonly RedisConfig _redisConfig;
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisService;
        private readonly IJsonSerializer _jsonSerializer;
        public CacheService(
            IOptions<RedisConfig> options,
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisService,
            IJsonSerializer jsonSerializer
            ) 
        {
            _redisConfig = options.Value;
            _logger = logger;
            _redisService = redisService;
            _jsonSerializer = jsonSerializer;
        }

        public Task AddOdUpdateAsync<T>(string key, T value) => 
            AddOdUpdateAsync(key, value);

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var serialized = await redis.StringGetAsync(key);

            return serialized.HasValue ?
                _jsonSerializer.Deserialize<T>(serialized.ToString()) :
                default(T)!;
        }
        
        private async Task AddorUpdateAsync<T>(
            string key,
            T value,
            IDatabase redis = null!,
            TimeSpan? expiry = null!)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _redisConfig.CacheTimeout;

            var serialized = _jsonSerializer.Serialize(value);
            
            
            if(await redis.StringSetAsync(key, serialized, expiry))
            {
                _logger.LogInformation($"Cached value for key {key} cached");
            }
            else
            {
                _logger.LogInformation($"Cached value for key {key} updated");
            }
        }

        private IDatabase GetRedisDatabase() =>
            _redisService.Connection.GetDatabase();
    }
}
