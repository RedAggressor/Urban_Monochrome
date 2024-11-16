using Basket.Host.Configs;
using Basket.Host.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplex;
        private bool _disposed = false;

        public RedisCacheConnectionService(IOptions<RedisConfig> config)
        {
            var redisConfigurationOption = ConfigurationOptions.Parse(config.Value.Host);

            //var redisConfigurationOption = new ConfigurationOptions
            //{
            //    EndPoints = { "localhost:6380" },
            //    ConnectTimeout = 5000,
            //};

            _connectionMultiplex = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(redisConfigurationOption));
        }
        public IConnectionMultiplexer Connection => _connectionMultiplex.Value;
        public void Dispose()
        {
            if(!_disposed)
            {
                _connectionMultiplex.Value.Dispose();
                _disposed = true;
            }
        }
    }
}
