using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services
{
    public class LikeItemService : BaseService<LikeItemService>, ILikeItemService
    {
        private readonly ICacheService _cacheService;
        private readonly string _keyLikeService = "LikeItemService";

        public LikeItemService(
            ICacheService cacheService,
            ILogger<LikeItemService> logger)
            : base(logger)
        {
            _cacheService = cacheService;
        }

        public async Task<BaseResponse> AddLikeItemsAsync(string key, DataRequest<ItemDto> items)
        {
            return await SafeExecuteAsync(async () =>
            {
                var keyRedis = $"{_keyLikeService}{key}";
                var result = await _cacheService.AddOrUpdateAsync(keyRedis, items.Data);

                return result;
            });
        }

        public async Task<DataResponse<ItemDto>> GetLikeItemsAsync(string key)
        {
            return await SafeExecuteAsync(async () =>
            {
                var keyRedis = $"{_keyLikeService}{key}";
                var result = await _cacheService.GetAsync<ICollection<ItemDto>>(keyRedis);

                return new DataResponse<ItemDto>
                { 
                    Data = result.ToList()
                };
            });
        }
    }
}
