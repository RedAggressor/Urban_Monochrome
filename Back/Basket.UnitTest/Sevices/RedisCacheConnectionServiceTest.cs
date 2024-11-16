using Basket.Host.Configs;
using Basket.Host.Services;

namespace Basket.UnitTest.Sevices
{
    public class RedisCacheConnectionServiceTest
    {
        private readonly Mock<Lazy<ConnectionMultiplexer>> _connectionMultiplexerMock;
        private readonly Mock<IOptions<RedisConfig>> _configRedis;
        private readonly RedisCacheConnectionService _redisCacheConnectionService;

        public RedisCacheConnectionServiceTest()
        {
            _configRedis = new Mock<IOptions<RedisConfig>>();
            _configRedis
                .Setup(st => st.Value)
                .Returns(new RedisConfig() { Host = "localhost:6380,abortConnect=false" });

            _connectionMultiplexerMock = new Mock<Lazy<ConnectionMultiplexer>>();

            _redisCacheConnectionService = new RedisCacheConnectionService(_configRedis.Object);

        }

        [Fact]
        public void Connection_Success()
        {            
            // act
            var connection = _redisCacheConnectionService.Connection;
            // assert

            connection.Should().NotBeNull();
            connection.Should().BeOfType<ConnectionMultiplexer>();
            connection.Configuration.Should().BeEquivalentTo("localhost:6380,abortConnect=false");

        }

        [Fact]
        public void Dispose_ShouldDisposeConnection()
        {
            // Arrange

            var connection = _redisCacheConnectionService.Connection;

            // Act

            _redisCacheConnectionService.Dispose();

            // Assert

            var isDisposed = (bool)typeof(RedisCacheConnectionService).
                GetField(
                    "_disposed",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)!
                .GetValue(_redisCacheConnectionService)!;

            isDisposed.Should().BeTrue();            
        }
    }
}
