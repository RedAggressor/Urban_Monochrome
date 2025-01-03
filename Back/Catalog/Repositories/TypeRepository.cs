using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly CatalogDbContext _dbContext;
        public TypeRepository(CatalogDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<int> AddTypeAsync(string name)
        {
            var entity = await _dbContext.Types.AddAsync(new TypeEntity 
            { 
                Name = name
            });

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<TypeEntity> GetTypeById(int id)
        {
            var entity = await _dbContext.Types
                .FirstOrDefaultAsync(type => type.Id == id);

            if(entity == null)
            {
                throw new Exception("Type doesn`t exthist or id enters inccorect");
            }

            return entity;
        }

        public async Task<string> DeleteTypeByIdAsync(int id)
        {
            var entity = await GetTypeById(id);

            var status = _dbContext.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return status.ToString();
        }

        public async Task<TypeEntity> UpdateTypeAsync(int id, string name)
        {
            var entity = await GetTypeById(id);
            entity.Name = name;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TypeEntity>> GetTypesAsync()
        {
            return await _dbContext.Types.ToListAsync();
        }
    }
}
