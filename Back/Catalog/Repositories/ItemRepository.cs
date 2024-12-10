using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.enums;
using Catalog.Host.Helper;
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

        public async Task<PaginatedItems<ItemEntity>> GetItemsByPageAsync(FilterItems filterItems)
        {
            var quary = _dbContext.Items.AsQueryable();

            quary = AddFiletrToQueryItem(filterItems);

            var itemOnPage = await quary.Select(x => x)
                .Include(i => i.Type)
                .Include(i => i.ItemSpecifications)
                    .ThenInclude(i => i.Size)
                .Include(i => i.ItemSpecifications)
                    .ThenInclude(i => i.Color)
                .Include(i => i.Groupe)
                .Skip(filterItems.PageIndex * filterItems.PageSize)
                .Take(filterItems.PageSize).ToListAsync();

            return new PaginatedItems<ItemEntity>()
            {
                Data = itemOnPage,
                TotalCountItem = itemOnPage.Count,
                PageIndex = filterItems.PageIndex,
                PageSize = filterItems.PageSize
            };
        }

        public async Task<ICollection<ItemEntity>?> GetItemsByNameAsync(string searchValue)
        {
            var words = searchValue.ToLower().Split(' ');

            var query = _dbContext.Items
           .Include(i => i.Type)
           .Include(i => i.ItemSpecifications)
               .ThenInclude(i => i.Size)
           .Include(i => i.ItemSpecifications)
               .ThenInclude(i => i.Color)
           .Include(i => i.Groupe)
           .AsQueryable();

            foreach (var word in words)
            {
                query = query.Where(f => 
                    f.Name.ToLower().Contains(word) ||
                    f.Type.Name.ToLower().Contains(word) ||
                    f.ItemSpecifications.Any(s => 
                        s.Color.Name.ToLower().Contains(word) ||
                        s.Size.Name.ToLower().Contains(word)));                
            }

            return await query.ToListAsync();
        }           

        public async Task<int> AddItemAsync(ItemEntity itemEntity)
        {
            itemEntity.CreatedAt = DateTime.UtcNow;
            itemEntity.UpdatedAt = DateTime.UtcNow;
            itemEntity.Discount = 0;

            var entity = await _dbContext.Items.AddAsync(itemEntity);

            if (itemEntity.ItemSpecifications is not null)
            {
                await _dbContext.ItemSpecifications.AddRangeAsync(

                    itemEntity.ItemSpecifications.Select(s => new ItemSpecificationEntity
                    {
                        ItemId = entity.Entity.Id,
                        ColorId = s.ColorId,
                        Quantity = s.Quantity,
                        SizeId = s.SizeId
                    }).ToList()
                );
            }

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<ItemEntity?> GetItemByIdAsync(int? id)
        {
            var result = await _dbContext.Items
            .Include(i => i.Type)
            .Include(i => i.ItemSpecifications)
                .ThenInclude(i => i.Size)
            .Include(i => i.ItemSpecifications)
                .ThenInclude(i => i.Color)
            .Include(i => i.Groupe)
            .FirstOrDefaultAsync(f => f.Id == id);

            if (result is null)
            {
                _logger.LogError("Id is wrong or item doesn`t exthist");
                throw new Exception("Id is wrong or item doesn`t exthist");
            }

            return result;
        }

        public async Task<string> DeleteItemByIdAsync(int? id)
        {
            var entity = await _dbContext.Items.FirstOrDefaultAsync(f => f.Id == id);

            var response = _dbContext.Items.Remove(entity!);

            await _dbContext.SaveChangesAsync();

            return response.State.ToString();
        }

        public async Task<ICollection<ItemEntity>?> GetItemsByIdAsync(List<int> listId)
        {
            var result = await _dbContext.Items
                .Include(i => i.Type)
                .Include(i => i.ItemSpecifications)
                    .ThenInclude(i => i.Size)
                .Include(i => i.ItemSpecifications)
                    .ThenInclude(i => i.Color)
                .Include(i => i.Groupe)
                .Where(item => listId.Contains(item.Id))
                .ToListAsync();

            if(result is null || result.Count() == 0)
            {
                _logger.LogWarning("the List of items don`t exthist");
                throw new Exception("the List of items don`t exthist");
            }

            return result;
        }

        public async Task<ItemEntity?> UpdateItemPropertyAsync(ItemEntity itemForUpdate)
        {
            var itemEntity = await GetItemByIdAsync(itemForUpdate.Id);        

            itemEntity!.Name = Chacked.IsNeedUpdate(itemEntity.Name, itemForUpdate.Name) ? itemForUpdate.Name : itemEntity.Name;
            itemEntity.Price = Chacked.IsNeedUpdate(itemEntity.Price, itemForUpdate.Price) ? itemForUpdate.Price : itemEntity.Price;
            itemEntity.Sex = Chacked.IsNeedUpdate(itemEntity.Sex, itemForUpdate.Sex) ? itemForUpdate.Sex : itemEntity.Sex;
            itemEntity.Description = Chacked.IsNeedUpdate(itemEntity.Description, itemForUpdate.Description) ? itemForUpdate.Description : itemEntity.Description;
            itemEntity.ImageUrl = Chacked.IsNeedUpdate(itemEntity.ImageUrl, itemForUpdate.ImageUrl) ? itemForUpdate.ImageUrl : itemEntity.ImageUrl;
            itemEntity.GroupeId = Chacked.IsNeedUpdate(itemEntity.GroupeId, itemForUpdate.GroupeId) ? itemForUpdate.GroupeId : itemEntity.GroupeId;
            itemEntity.TypeId = Chacked.IsNeedUpdate(itemEntity.TypeId, itemForUpdate.TypeId) ? itemForUpdate.TypeId : itemEntity.TypeId;

            foreach(var spec in itemForUpdate.ItemSpecifications)
            {
                var entity = await _dbContext.ItemSpecifications.FirstOrDefaultAsync(s => s.Id == spec.Id);
                if(entity != null && Chacked.IsNeedUpdate(entity, spec))
                {
                    var sizeEntity = await _dbContext.Sizes.FirstOrDefaultAsync(f=> f.Id == spec.SizeId);
                    if (sizeEntity is null)
                    {
                        throw new Exception("Size not exthist!");
                    }

                    var colorEntity = await _dbContext.Colors.FirstOrDefaultAsync(f => f.Id == spec.ColorId);
                    if(colorEntity is null)
                    {
                        throw new Exception("Color not exthist");
                    }

                    if(spec.Quantity < 0)
                    {
                        throw new Exception("Quantity cann`t be negative");
                    }

                    entity.SizeId = Chacked.IsNeedUpdate(entity.SizeId, spec.SizeId) ? spec.SizeId : entity.SizeId;
                    entity.ColorId = Chacked.IsNeedUpdate(entity.ColorId, spec.ColorId) ? spec.ColorId : entity.ColorId;
                    entity.Quantity = Chacked.IsNeedUpdate(entity.Quantity, spec.Quantity) ? spec.Quantity : entity.Quantity;                    
                }
                else if(entity == null)
                {
                    spec.ItemId = itemEntity.Id;
                    itemEntity.ItemSpecifications.Add(spec);
                }
            };

            var deleteSpec = itemEntity.ItemSpecifications
                .Where(spec => !itemForUpdate.ItemSpecifications.Any(a => a.Id == spec.Id)).ToList();

            foreach(var spec in deleteSpec)
            {                
                itemEntity.ItemSpecifications.Remove(spec);
            }

            itemEntity.UpdatedAt = DateTime.UtcNow;
           
            await _dbContext.SaveChangesAsync();

            return itemEntity;
        }

        private IQueryable<ItemEntity> AddFiletrToQueryItem(FilterItems filterItems)
        {
            var quary = _dbContext.Items.AsQueryable();

            if (filterItems is null || filterItems == DefaultValue.GetDefaultValue(filterItems))
            {
                return quary;
            }

            if (filterItems.SortBy is not SortByType.None)
            {
                quary = filterItems.SortBy == SortByType.Ascending ?
                    quary.OrderBy(o => o.Price) :
                    quary.OrderByDescending(o => o.Price);
            }

            if (filterItems.SizeFiltersById is not null && filterItems.SizeFiltersById.Any())
            {
                quary = quary.Where(item =>
                    item.ItemSpecifications.Any(i =>
                    filterItems.SizeFiltersById.Contains(i.Size.Id)));
            }

            if (filterItems.GroupeFiltersById is not null && filterItems.GroupeFiltersById.Any())
            {
                quary = quary.Where(item =>
                    filterItems.GroupeFiltersById.Contains(item.Groupe.Id));
            }

            if (filterItems.ColorFilterById != DefaultValue.GetDefaultValue(filterItems.ColorFilterById))
            {
                quary = quary.Where(item =>
                    item.ItemSpecifications.Any(i =>
                        i.Color.Id == filterItems.ColorFilterById));
            }

            if (filterItems.SexFilters is not SexType.None)
            {
                quary = quary.Where(item =>
                    item.Sex == filterItems.SexFilters);
            }

            if (filterItems.TypeFiltersById is not null && filterItems.TypeFiltersById.Any())
            {
                quary = quary.Where(item =>
                    filterItems.TypeFiltersById.Contains(item.Type.Id));
            }

            if (filterItems?.HotItems?.New ?? false)
            {
                var groupe = quary.OrderByDescending(item => item.Groupe.CreatedAt).FirstOrDefault()!.Groupe;
                quary = quary.Where(item => item.GroupeId == groupe!.Id);
            }

            if (filterItems?.HotItems?.BestSeller ?? false)
            {
                quary = quary.OrderBy(i => i.Sold);
            }

            if (filterItems?.HotItems?.Sold ?? false)
            {
                quary = quary.OrderBy(i => i.Discount);
            }

            if (filterItems?.PriceFilter is not null)
            {
                var to = filterItems.PriceFilter.To > filterItems.PriceFilter.From ?
                    filterItems.PriceFilter.To :
                    filterItems.PriceFilter.From;

                var from = filterItems.PriceFilter.From < filterItems.PriceFilter.To ?
                    filterItems.PriceFilter.From :
                    filterItems.PriceFilter.To;

                quary = quary.Where(item => item.Price >= from && item.Price <= to);
            }

            return quary;
        }
    }
}
