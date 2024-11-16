using Basket.Host.Configs;
using Basket.Host.Services.Interfaces;
using StackExchange.Redis;
using Microsoft.Extensions.Options;

namespace Basket.Host.Services
{
    public class CacheService : BaseService<CacheService>, ICacheService
    {
        private readonly RedisConfig _redisConfig;
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisService;
        private readonly IJsonSerializerService _jsonSerializer;

        public CacheService(
            IOptions<RedisConfig> options,
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisService,
            IJsonSerializerService jsonSerializer
            )
            : base(logger)
        {
            _redisConfig = options.Value;
            _logger = logger;
            _redisService = redisService;
            _jsonSerializer = jsonSerializer;
        }

        public Task<BaseResponse> AddOrUpdateAsync<T>(string key, T value)
        {
            return SafeExecuteAsync(() =>
                AddorUpdateInterAsync(key, value)
            );
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var serialized = await redis.StringGetAsync(key);

            return serialized.HasValue ?
                _jsonSerializer.Deserialize<T>(serialized.ToString()) :
                default(T)!;
        }

        private async Task<BaseResponse> AddorUpdateInterAsync<T>(
            string key,
            T value,
            IDatabase redis = null!,
            TimeSpan? expiry = null!)
        {
            return await SafeExecuteAsync(async () =>
            {
                redis ??= GetRedisDatabase();
                expiry ??= _redisConfig.CacheTimeout;

                var serialized = _jsonSerializer.Serialize(value);


                if (await redis.StringSetAsync(key, serialized, expiry))
                {
                    _logger.LogInformation($"Cached value for key {key} cached");
                    return new BaseResponse();
                }
                else
                {
                    _logger.LogInformation($"Cached value for key {key} updated");
                    return new BaseResponse()
                    {
                        ErrorMessage = $"Cached value for key {key} updated"
                    };

                }
            });
        }

        private IDatabase GetRedisDatabase() =>
            _redisService.Connection.GetDatabase();
    }
}
