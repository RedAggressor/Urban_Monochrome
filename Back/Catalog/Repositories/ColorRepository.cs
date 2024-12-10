using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ColorRepository(IDbContextWrapper<CatalogDbContext> dbContext)
        {
            _dbContext = dbContext.DbContext;
        }

        public async Task<int> AddColorAsync(string name)
        {
            var entity = await _dbContext.Colors.AddAsync(
                new ColorEntity 
                { 
                    Name = name
                });

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<ColorEntity> GetColorByIdAsync(int id)
        {
            var entity = await _dbContext.Colors.FirstOrDefaultAsync(f=>f.Id == id);

            if (entity is null)
            {
                throw new Exception("Color don`t exthist");
            }

            return entity;
        }

        public async Task<string> DeleteColorAsync(int id)
        {
            var colorEntity = await GetColorByIdAsync(id);

            var status = _dbContext.Colors.Remove(colorEntity);

            await _dbContext.SaveChangesAsync();

            return status.ToString();
        }

        public async Task<ColorEntity> UpdateColorAsync(int id, string newName)
        {
            var entity = await GetColorByIdAsync(id);

            entity.Name = newName;

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<ICollection<ColorEntity>> GetColorsAsync()
        {
            return await _dbContext.Colors.ToListAsync();
        }
    }
}
