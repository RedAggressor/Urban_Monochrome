using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Catalog.Host.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<IItemRepository> _logger;
        public ItemRepository(
            IDbContextFactory<ApplicationDbContext> dbContext,
            ILogger<IItemRepository> logger)
        {
            _dbContext = dbContext.CreateDbContext();
            _logger = logger;
        }

        public async Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int indexPage, int pageSize)
        {
            var quary = _dbContext.Items;
            var totalPage = await quary.LongCountAsync();

            var itemOnPage = await quary
                .Select(x => x)
                .Skip(indexPage * pageSize)
                .Take(pageSize).ToListAsync();

            return new PaginatedItems<ItemEntity>()
            {
                Data = itemOnPage,
                TotalCountItem = totalPage,
                PageIndex = indexPage,
                PageSize = pageSize
            };
        }

        public async Task GetItemsOrderByPrice()
        {

        }
    }
}
