using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.enums;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemRepository
    {
        Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int indexPage, int pageSize, OrderType orderType);
        Task<ItemEntity> GetItemsByName(string name);
        Task<int> AddItemAsync(ItemEntity itemEntity);
        Task<ItemEntity> GetItemById(int id);
        Task<string> DeleteItemById(int id);
    }
}
