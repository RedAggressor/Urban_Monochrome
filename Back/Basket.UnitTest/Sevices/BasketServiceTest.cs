using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;


namespace Basket.UnitTest.Sevices
{
    public class BasketServiceTest
    {
        private IBasketService _service;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<BasketService>> _logger;

        public BasketServiceTest()
        {
            _cacheService = new Mock<ICacheService>();
            _logger = new Mock<ILogger<BasketService>>();
            _service = new BasketService(_cacheService.Object, _logger.Object);
        }

        [Fact]
        public async Task AddDataAsync_Success()
        {
            // arrange
            var testEntity = new DataRequest<Item>()
            {
                Data = new List<Item>
                {
                    new Item
                    {
                        Id = 1,
                        //Color = "testColor",
                        Name = "testName",
                        //Type = new TypeDto()
                        //{
                        //    Id = 1,
                        //    Name = "test"
                        //},
                        //NestedType = new NestedTypeDto()
                        //{
                        //    Id = 1,
                        //    Name = "test"
                        //}
                    }
                }
            };

            var testKey = "testKey";
            var testRedis = $"BasketService{testKey}";
            var response = new BaseResponse();


            _cacheService
                .Setup(s => s.AddOrUpdateAsync(
                    It.IsAny<string>(),
                    It.IsAny<DataRequest<Item>>()))
                .ReturnsAsync(response);

            // act

            var result = await _service.AddDataAsync(testKey, testEntity);

            // assert
            result.Should().NotBeNull();
            result.ResponseCodeType.Should().Be(ResponseCodeType.Success);
            result.ErrorMessage.Should().BeNullOrEmpty();
            _cacheService.Verify(s=>s.AddOrUpdateAsync(testRedis, testEntity), Times.Once);
        }

        [Fact]
        public async Task AddDataAsync_Failed()
        {
            //arrange

            var testEntity = new DataRequest<Item>()
            {
                Data = null!
            };

            string? testKey = null;
            var testRedis = $"BasketService{testKey}";
            var response = new BaseResponse()
            { 
                 ErrorMessage = "Test error"
            };


            _cacheService
                .Setup(s => s.AddOrUpdateAsync(
                    It.IsAny<string>(),
                    It.IsAny<DataRequest<Item>>()))
                .ThrowsAsync(new Exception("Test error"));

            //act

            var result = await _service.AddDataAsync(testKey, testEntity);

            //assert

            result.Should().NotBeNull();
            result.ErrorMessage.Should().NotBeNullOrEmpty();
            result.ResponseCodeType.Should().Be(ResponseCodeType.Failed);
            _cacheService
                .Verify(v => v.AddOrUpdateAsync(testRedis, testEntity), Times.Once);
        }

        [Fact]
        public async Task GetDataAsync_Success()
        {
            //arrange

            var testGetData = new DataResponse<Item>()
            {
                Data = new List<Item>()
                {
                    new Item()
                    {
                        Id = 1,
                        Name = "Test",
                        //Type = new TypeDto()
                        //{
                        //    Id = 1,
                        //    Name = "Test",
                        //},
                        //NestedType = new NestedTypeDto()
                        //{
                        //    Id = 1,
                        //    Name = "Test",
                        //}
                    }
                }
            };

            var testkey = "test";
            var redisTestKey = $"BasketService{testkey}";

            _cacheService
                .Setup(st => 
                    st.GetAsync<DataResponse<Item>>(It.IsAny<string>()))
                .ReturnsAsync(testGetData);

            //act

            var response = await _service.GetDataAsync(testkey);

            //asert

            response.Should().NotBeNull();
            response.ErrorMessage.Should().BeNullOrEmpty();
            response.ResponseCodeType.Should().Be(ResponseCodeType.Success);
            response.Data.Should().NotBeNull();
            response.Data[0].Id.Should().Be(1);
            response.Data[0].Name.Should().Be("Test");
            response.Data[0].Type.Should().NotBeNull();
            //response.Data[0].NestedType.Should().NotBeNull();
            //response.Data[0].Type.Name.Should().Be("Test");
            //response.Data[0].NestedType.Name.Should().Be("Test");
            _cacheService
                .Verify(ve => ve.GetAsync<DataResponse<Item>>(
                    It.Is<string>(s => s.Equals(redisTestKey))),
                    Times.Once);
        }

        [Fact]
        public async Task GetDataAsync_Failed()
        {
            //arrange
            string? testKey = null;
            var redisTestKey = $"BasketService{testKey}";
            var testResponse = new BaseResponse()
            {
                ErrorMessage = "test error"
            };

            _cacheService
                .Setup(st => st.GetAsync<DataResponse<Item>>(
                    It.IsAny<string>()))
                .ThrowsAsync(new Exception("test error"));

            //act

            var result = await _service.GetDataAsync(testKey);

            //asert

            result.Should().NotBeNull();
            result.ResponseCodeType.Should().Be(ResponseCodeType.Failed);
            result.ErrorMessage.Should().NotBeNullOrEmpty();
            result.ErrorMessage.Should().Be("test error");
            _cacheService.Verify(ve => 
                ve.GetAsync<DataResponse<Item>>(redisTestKey),
                Times.Once);
        }
    }
}
