using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        Task<DataResponse<UniqueItemResponse>> GetDataAsync(string key);
        Task<BaseResponse> AddDataAsync(string key, DataRequest<UniqueItemResponse> data);
    }
}
