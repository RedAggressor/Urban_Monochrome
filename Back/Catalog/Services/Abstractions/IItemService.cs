using Catalog.Host.Models.Dto;

namespace Catalog.Host.Services.Abstractions
{
    public interface IItemService
    {
        Task<DataResponse<int>> AddItemAsync(DataRequest<ItemDto> request);
        Task<DataResponse<ItemDto>> GetItemByIdAsync(DataRequest<int> request);
        Task<DataResponse<string>> DeleteItemByIdAsync(DataRequest<int> request);
        Task<DataResponse<ItemDto>> UpdateOrChangeItemAsync(DataRequest<ItemDto> request);
    }
}
