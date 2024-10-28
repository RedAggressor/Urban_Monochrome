using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Abstractions
{
    public interface IItemService
    {
        Task<DataResponse<int>> AddItemAsync(ItemDto itemDto);
        Task<DataResponse<ItemDto>> GetItemByIdAsync(int? id);
        Task<DataResponse<string>> DeleteItemByIdAsync(int? id);
        Task<DataResponse<ItemDto>> UpdateOrChangeItemAsync(ItemDto itemDto);
    }
}
