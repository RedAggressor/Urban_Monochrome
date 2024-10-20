using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemRepository
    {
        Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int indexPage, int pageSize);
    }
}
