using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;

namespace Basket.Host.Services.Interfaces
{
    public interface ILikeItemService
    {
        Task<BaseResponse> AddLikeItemsAsync(string key, DataRequest<ItemDto> items);
        Task<DataResponse<ItemDto>> GetLikeItemsAsync(string key);
    }
}
