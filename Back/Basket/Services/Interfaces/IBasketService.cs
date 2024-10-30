using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        Task<DataResponse<ItemDto>> GetDataAsync(string key);
        Task<BaseResponse> AddDataAsync(string key, DataRequest<ItemDto> data);
    }
}
