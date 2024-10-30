using Basket.Host.Configs;
using Basket.Host.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplex;
        private bool _disposed = false;

        public RedisCacheConnectionService(IOptions<RedisConfig> config)
        {
            var redisConfigurationOption = ConfigurationOptions.Parse(config.Value.Host);

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
