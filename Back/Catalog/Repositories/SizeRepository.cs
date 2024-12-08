using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly CatalogDbContext _dbContext;

        public SizeRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddSizeAsync(string name)
        {
            var entity = await _dbContext.Sizes.AddAsync(new SizeEntity
            {
                Name = name                
            });

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<SizeEntity> GetSizeByIdAsync(int id)
        {
            var entity = await _dbContext.Sizes.FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null) 
            {
                throw new Exception("Set wrong id or size don`t exthist");
            }

            return entity;
        }


        public async Task<string> DeleteSizeByIdAsync(int id)
        {
            var entity = await GetSizeByIdAsync(id);

            var status = _dbContext.Sizes.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return status.ToString();
        }

        public async Task<SizeEntity> UpdateSizeAsync(int id, string name)
        {
            var entity = await GetSizeByIdAsync(id);

            entity.Name = name;

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
