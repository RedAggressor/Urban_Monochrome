using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services
{
    public class BasketService
    {
        private readonly ICacheService _cacheService;
        private readonly string _keyBasket = "basket";
        private readonly ILogger<BasketService> _logger;
        public BasketService(
            ICacheService cacheService,
            ILogger<BasketService> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task<BaseResponse> AddDataAsync(string key, DataRequest<ItemDto> data)
        {
            try 
            {
                var keyRedis = $"{_keyBasket}{key}";
                await _cacheService.AddOdUpdateAsync(keyRedis, data);
                return new BaseResponse();
            }
            catch (Exception ex) 
            { 
                return new BaseResponse()
                { 
                    ErrorMessage = ex.Message,
                };
            }         
        }

        public async Task<DataResponse<ItemDto>> GetDataAsync(string key)
        {
            try
            {
                var keyRedis = $"{_keyBasket}{key}";

                var response = await _cacheService.GetAsync<DataResponse<ItemDto>>(keyRedis);

                return response;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw;
            }

            
        }
    }
}
