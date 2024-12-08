using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemRepository
    {
        Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(FilterItems filterItems);
        Task<ICollection<ItemEntity>?> GetItemsByNameAsync(string searchValue);
        Task<int> AddItemAsync(ItemEntity itemEntity);
        Task<ItemEntity?> GetItemByIdAsync(int? id);
        Task<string> DeleteItemByIdAsync(int? id);
        Task<ItemEntity?> UpdateItemPropertyAsync(ItemEntity itemForUpdate);
        Task<ICollection<ItemEntity>?> GetItemsByIdAsync(List<int> listId);
    }
}
