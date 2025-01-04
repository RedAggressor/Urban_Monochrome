using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Infrastucture.Models.Item;

namespace Basket.Host.Services
{
    public class BasketService : BaseService<BasketService>, IBasketService
    {
        private readonly ICacheService _cacheService;
        private readonly string _keyBasket = "BasketService";
        public BasketService(
            ICacheService cacheService,
            ILogger<BasketService> logger) : base(logger)
        {
            _cacheService = cacheService;
        }
        public async Task<BaseResponse> AddDataAsync(string key, DataRequest<UniqueItemResponse> data)
        {
            return await SafeExecuteAsync(async () =>
            {
                var keyRedis = $"{_keyBasket}{key}";
                return await _cacheService.AddOrUpdateAsync(keyRedis, data);
            });
        }

        public async Task<DataResponse<UniqueItemResponse>> GetDataAsync(string key)
        {
            return await SafeExecuteAsync(async () =>
            {
                var keyRedis = $"{_keyBasket}{key}";
                return await _cacheService.GetAsync<DataResponse<UniqueItemResponse>>(keyRedis);
            });
        }
    }
}
