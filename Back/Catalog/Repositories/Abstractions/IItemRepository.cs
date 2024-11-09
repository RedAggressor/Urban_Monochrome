using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.enums;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemRepository
    {
        Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int indexPage, int pageSize, SortByType? orderType = SortByType.None, List<string>? typeFilters = null, List<string>? nestedFilters = null);
        Task<ItemEntity?> GetItemsByNameAsync(string name);
        Task<int> AddItemAsync(ItemEntity itemEntity);
        Task<ItemEntity?> GetItemByIdAsync(int? id);
        Task<string> DeleteItemByIdAsync(int? id);
        Task<ItemEntity?> UpdateOrChangeItemAsync(ItemEntity itemForUpdate);
        Task<ICollection<ItemEntity>?> GetItemsByIdAsync(List<int> listId);
    }
}
