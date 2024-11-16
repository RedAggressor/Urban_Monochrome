using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.enums;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly CatalogDbContext _dbContext;
        private readonly ILogger<IItemRepository> _logger;
        public ItemRepository(
            IDbContextFactory<CatalogDbContext> dbContext,
            ILogger<IItemRepository> logger)
        {
            _dbContext = dbContext.CreateDbContext();
            _logger = logger;
        }

        public async Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(
            int indexPage,
            int pageSize,
            SortByType? orderType = SortByType.None,
            List<string>? typeFilters = null,
            List<string>? nestedTypeFilters = null)
        {
            var quary = _dbContext.Items.AsQueryable();

            if(orderType != SortByType.None)
            {
                quary = orderType == SortByType.Ascending ?
                    quary.OrderBy(o => o.Price) :
                    quary.OrderByDescending(o => o.Price);
            }                

            if (typeFilters is not null)
            {
                quary = quary.Where(item => typeFilters.Contains(item.Type.Name));                
            }

            if(nestedTypeFilters is not null)
            {                
                 quary = quary.Where(item => nestedTypeFilters.Contains(item.NestedType.Name));               
            }

            var itemOnPage = await quary.Select(x => x)
                .Include(i => i.Type)
                .Include(i => i.NestedType)
                .ThenInclude(i=>i.Type)
                .Skip(indexPage * pageSize)
                .Take(pageSize).ToListAsync();

            return new PaginatedItems<ItemEntity>()
            {
                Data = itemOnPage,
                TotalCountItem = itemOnPage.Count,
                PageIndex = indexPage,
                PageSize = pageSize
            };
        }

        public async Task<ItemEntity?> GetItemsByNameAsync(string name) => 
            await _dbContext.Items
            .Include(i=>i.Type)
            .Include(i=>i.NestedType)
            .ThenInclude(i => i.Type)
            .FirstOrDefaultAsync(f=>f.Name.Contains(name));

        public async Task<int> AddItemAsync(ItemEntity itemEntity)
        {
            var entity = await _dbContext.Items.AddAsync(itemEntity);

            await _dbContext.SaveChangesAsync();    

            return entity.Entity.Id;
        }
        
        public async Task<ItemEntity?> GetItemByIdAsync(int? id) =>
            await _dbContext.Items
            .Include(i=>i.Type)
            .Include(t=>t.NestedType)
            .ThenInclude(i => i.Type)
            .FirstOrDefaultAsync(f=>f.Id == id);
        
        public async Task<string> DeleteItemByIdAsync(int? id)
        {
            var entity = await _dbContext.Items.FirstOrDefaultAsync(f => f.Id == id);

            var response = _dbContext.Items.Remove(entity!);

            await _dbContext.SaveChangesAsync();

            return response.State.ToString();
        }

        public async Task<ICollection<ItemEntity>?> GetItemsByIdAsync(List<int> listId)
        {
            return await _dbContext.Items
                .Include(inc => inc.NestedType)
                .ThenInclude(i=>i.Type)
                .Include(inc => inc.Type)
                .Where(item => listId.Contains(item.Id))
                .ToListAsync();
        }        

        public async Task<ItemEntity?> UpdateOrChangeItemAsync(ItemEntity itemForUpdate)
        {
            var itemEntity = await _dbContext.Items
                .Include(item => item.Type)
                .Include(item => item.NestedType)
                .ThenInclude(it=>it.Type)
                .FirstOrDefaultAsync(f => f.Id == itemForUpdate.Id);
                        
            itemEntity!.Name = (itemEntity.Name == itemForUpdate.Name) || (itemForUpdate.Name == GetDefaualtValue(itemForUpdate.Name)) ? itemEntity.Name : itemForUpdate.Name;
            itemEntity.Price = (itemEntity.Price == itemForUpdate.Price) || (itemForUpdate.Price == GetDefaualtValue(itemForUpdate.Price)) ? itemEntity.Price : itemForUpdate.Price;
            itemEntity.Quantity = (itemEntity.Quantity == itemForUpdate.Quantity) || (itemForUpdate.Quantity == GetDefaualtValue(itemForUpdate.Quantity)) ? itemEntity.Quantity : itemForUpdate.Quantity;
            itemEntity.Sex = (itemEntity.Sex == itemForUpdate.Sex) || (itemForUpdate.Sex == GetDefaualtValue(itemForUpdate.Sex)) ? itemEntity.Sex : itemForUpdate.Sex;
            itemEntity.Size = (itemEntity.Size == itemForUpdate.Size) || (itemForUpdate.Size == GetDefaualtValue(itemForUpdate.Size)) ? itemEntity.Size : itemForUpdate.Size;
            itemEntity.Color = (itemEntity.Color == itemForUpdate.Color) || (itemForUpdate.Color == GetDefaualtValue(itemForUpdate.Color)) ? itemEntity.Color : itemForUpdate.Color;
            itemEntity.Description = (itemEntity.Description == itemForUpdate.Description) || (itemForUpdate.Description == GetDefaualtValue(itemForUpdate.Description)) ? itemEntity.Description : itemForUpdate.Description;
            itemEntity.ImageUrl = (itemEntity.ImageUrl == itemForUpdate.ImageUrl) || (itemForUpdate.ImageUrl == GetDefaualtValue(itemForUpdate.ImageUrl)) ? itemEntity.ImageUrl : itemForUpdate.ImageUrl;
            itemEntity.NestedTypeId = (itemEntity.NestedTypeId == itemForUpdate.NestedTypeId) || (itemForUpdate.NestedTypeId == GetDefaualtValue(itemForUpdate.NestedTypeId)) ? itemEntity.NestedTypeId : itemForUpdate.NestedTypeId;
            itemEntity.TypeId = (itemEntity.TypeId == itemForUpdate.TypeId) || (itemForUpdate.TypeId == GetDefaualtValue(itemForUpdate.TypeId)) ? itemEntity.TypeId : itemForUpdate.TypeId;

            await _dbContext.SaveChangesAsync();
            return itemEntity;
        }

        private T GetDefaualtValue<T>(T obj) => default(T)!;
    }
}
