using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Helper;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class ItemSpecificationRepository : IItemSpecificationRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ItemSpecificationRepository(CatalogDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<UniqueItemEntity> GetSpecificationByIdAsync(int id)
        {
            var result = await _dbContext.UniqueItems
                .Include(i => i.Color)
                .Include(i=>i.Item)
                    .ThenInclude(z=>z.Groupe)
                .Include(i=>i.Item)
                    .ThenInclude(z=>z.Type)
                .Include(i=>i.Size)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(result is null)
            {
                throw new Exception("Id is wrong or specification doesn`t exthist");
            }

            return result;
        }

        public async Task<int> AddSpecificationAsync(UniqueItemEntity entity)
        {
            var result = await _dbContext.UniqueItems.AddAsync(new UniqueItemEntity 
            { 
                ItemId = entity.ItemId,
                ColorId = entity.ColorId,
                SizeId = entity.SizeId,
                Quantity = entity.Quantity
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<string> DeleteSpecificationAsync(int id)
        {
            var entity = await GetSpecificationByIdAsync(id);

            var status = _dbContext.UniqueItems.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return status.ToString();
        }

        public async Task<UniqueItemEntity> UpdateSpecificationAsync(UniqueItemEntity specForUpdate)
        {
            var entity = await GetSpecificationByIdAsync(specForUpdate.Id);

            entity.ColorId = Chacked.IsNeedUpdate(entity.ColorId, specForUpdate.ColorId) ? specForUpdate.ColorId : entity.ColorId;
            entity.SizeId = Chacked.IsNeedUpdate(entity.SizeId, specForUpdate.SizeId) ? specForUpdate.SizeId : entity.SizeId;
            entity.ItemId = Chacked.IsNeedUpdate(entity.ItemId, specForUpdate.ItemId) ? specForUpdate.ItemId : entity.ItemId;
            entity.Quantity = Chacked.IsNeedUpdate(entity.Quantity, specForUpdate.Quantity) ? specForUpdate.Quantity : entity.Quantity;

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<ICollection<UniqueItemEntity>> GetSpecificationsByIdAsync(List<int> idList)
        {
            return await _dbContext.UniqueItems
                .Include(i => i.Color)
                .Include(i => i.Item)
                    .ThenInclude(z => z.Groupe)
                .Include(i => i.Item)
                    .ThenInclude(z => z.Type)
                .Include(i => i.Size)
                .Where(spec => idList.Contains(spec.Id))
                .ToListAsync();
        }
    }
}
