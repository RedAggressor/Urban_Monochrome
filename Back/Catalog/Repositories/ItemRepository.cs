using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.enums;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(int indexPage, int pageSize, OrderType orderType)
        {
            var quary = _dbContext.Items.AsQueryable();
            var totalPage = await quary.LongCountAsync();

            if(orderType != OrderType.None)
            {
                quary = orderType == OrderType.Ascending ?
                    quary.OrderBy(o => o.Price) :
                    quary.OrderByDescending(o => o.Price);
            }           

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

        public async Task<ItemEntity> GetItemsByName(string name) => 
            await _dbContext.Items.FirstOrDefaultAsync(f=>f.Name == name);

        public async Task<int> AddItemAsync(ItemEntity itemEntity)
        {
            var entity = await _dbContext.Items.AddAsync(itemEntity);

            await _dbContext.SaveChangesAsync();    

            return entity.Entity.Id;
        }
        
        public async Task<ItemEntity> GetItemById(int id) =>
            await _dbContext.Items.FirstOrDefaultAsync(f=>f.Id == id);
        
        public async Task<string> DeleteItemById(int id)
        {
            var entity = await _dbContext.Items.FirstOrDefaultAsync(f => f.Id == id);

            var response = _dbContext.Items.Remove(entity!);

            await _dbContext.SaveChangesAsync();

            return response.State.ToString();
        }
    }
}
