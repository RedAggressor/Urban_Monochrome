using Basket.Host.Configs;
using Basket.Host.Models.Dto;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;

namespace Basket.UnitTest.Sevices
{
    public class CacheServiceTest
    {
        private readonly ICacheService _cacheService;

        private readonly Mock<IOptions<RedisConfig>> _redisConfig;
        private readonly Mock<ILogger<CacheService>> _logger;
        private readonly Mock<IRedisCacheConnectionService> _redisConnectionService;
        private readonly Mock<IJsonSerializerService> _jsonSerizlizer;
        private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer;
        private readonly Mock<IDatabase> _database;

        public CacheServiceTest()
        {
            _redisConfig = new Mock<IOptions<RedisConfig>>();
            
            _logger = new Mock<ILogger<CacheService>>();
            _redisConnectionService = new Mock<IRedisCacheConnectionService>();
            _jsonSerizlizer = new Mock<IJsonSerializerService>();
            _database = new Mock<IDatabase>();
            _connectionMultiplexer = new Mock<IConnectionMultiplexer>();

            _connectionMultiplexer
                .Setup(s=>s.GetDatabase(
                    It.IsAny<int>(),
                    It.IsAny<object>()))
                .Returns(_database.Object);

            _redisConnectionService
                .Setup(s=>s.Connection)
                .Returns(_connectionMultiplexer.Object);

            _redisConfig
                .Setup(x => x.Value)
                .Returns(new RedisConfig() { CacheTimeout = TimeSpan.Zero });

            _cacheService = new CacheService(
                _redisConfig.Object,
                _logger.Object,
                _redisConnectionService.Object,
                _jsonSerizlizer.Object);
        }

        [Fact]
        public async Task AddOrUpdateAsync_Success()
        {
            //arrange
            var keyTest = "keyTest";
            var testData = new List<ItemDto>()
            {
                new ItemDto()
                {
                     Id = 1,
                     Name = "Test",
                     Type = new TypeDto() { Id = 1, Name = "Test" },
                     NestedType = new NestedTypeDto() { Id = 1, Name = "Test"}
                }
            };
            var serializeDataTest = JsonConvert.SerializeObject(testData);
                       

            _database.Setup(r => r.StringSetAsync(
                keyTest,
                serializeDataTest,
                TimeSpan.Zero,
                false,
                When.Always,
                CommandFlags.None
               )).ReturnsAsync(false);            

            _jsonSerizlizer
                .Setup(st=>st.Serialize(It.IsAny<List<ItemDto>>()))
                .Returns(serializeDataTest);

            //act

            var result = await _cacheService.AddOrUpdateAsync(keyTest, testData);

            //asert

            result.Should().NotBeNull();
            result.Should().BeOfType<BaseResponse>();
            result.ResponseCodeType.Should().Be(ResponseCodeType.Success);
            result.ErrorMessage.Should().BeNullOrEmpty();
            _database
                .Verify(r => r.StringSetAsync(
                    keyTest,
                    serializeDataTest,
                    TimeSpan.Zero, 
                    false,
                    When.Always,
                    CommandFlags.None
                   ), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_Failed()
        {
            //arrange

            string? testKey = null;

            var testData = new List<ItemDto>()
            {
                new ItemDto()
                {
                     Id = 1,
                     Name = "Test",
                     Type = new TypeDto() { Id = 1, Name = "Test" },
                     NestedType = new NestedTypeDto() { Id = 1, Name = "Test"}
                }
            };

            _database.Setup(expression : r => r.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                TimeSpan.Zero,
                false,
                When.Always,
                CommandFlags.None
               )).ReturnsAsync(false);

            //act

            var result = await _cacheService.AddOrUpdateAsync(testKey, testData);

            //asert

            result.Should().NotBeNull();
            result.Should().BeOfType<BaseResponse>();
            result.ErrorMessage.Should().NotBeNullOrEmpty();
            result.ResponseCodeType.Should().Be(ResponseCodeType.Failed);
            _database.Verify(v => v.StringSetAsync(It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                TimeSpan.Zero,
                false,
                When.Always,
                CommandFlags.None), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            //arrange
            var testKey = "testKey";
            var testData = new List<ItemDto>()
            {
                new ItemDto()
                {
                     Id = 1,
                     Name = "Test",
                     Type = new TypeDto() { Id = 1, Name = "Test" },
                     NestedType = new NestedTypeDto() { Id = 1, Name = "Test"}
                }
            };

            var serializeTestData = JsonConvert.SerializeObject(testData);
            var redisValue = (RedisValue)serializeTestData;

            _database
                .Setup(s=>s.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(redisValue);

            _jsonSerizlizer
                .Setup(s=>s.Deserialize<List<ItemDto>>(It.IsAny<string>()))
                .Returns(testData);

            //act

            var result = await _cacheService.GetAsync<List<ItemDto>>(testKey);

            //asert

            result.Should().NotBeNull();
            result.ForEach(item => item.Should().NotBeNull());
            result.Count.Should().Be(testData.Count);            
            for (int i = 0; i < testData.Count; i++) 
            { 
                result[i].Id.Should().Be(testData[i].Id);
                result[i].Name.Should().Be(testData[i].Name);
                result[i].Type.Should().BeEquivalentTo(testData[i].Type);
                result[i].NestedType.Should().BeEquivalentTo(testData[i].NestedType); 
            }
            _database.Verify(v=>v.StringGetAsync(testKey, CommandFlags.None), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Failed()
        {
            //arrage

            string? testKey = null;

            _database
                .Setup(st => st.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(RedisValue.Null);

            //act

            var result = await _cacheService.GetAsync<List<ItemDto>>(testKey);

            //asert

            result.Should().BeNull();
            _database.Verify(v => v.StringGetAsync(testKey, CommandFlags.None), Times.Once);
        }
    }
}
