using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Abstractions
{
    public interface IItemService
    {
        Task<DataResponse<int>> AddItemAsync(DataRequest<ItemDto> itemDto);
        Task<DataResponse<ItemDto>> GetItemByIdAsync(DataRequest<int> data);
        Task<DataResponse<string>> DeleteItemByIdAsync(DataRequest<int> data);
    }
}
