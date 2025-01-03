using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class GroupeRepository : IGroupeRepository
    {
        private readonly CatalogDbContext _dbContext;
        public GroupeRepository(CatalogDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<int> AddGroupeAsync(string name)
        {
            var entity = await _dbContext.Groupes.AddAsync(new GroupeEntity
            { 
                Name = name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
            await _dbContext.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<GroupeEntity> GetGroupeByIdAsync(int id)
        {
            var entity = await _dbContext.Groupes
                .FirstOrDefaultAsync(f => f.Id == id);

            if(entity is null)
            {
                throw new Exception("Groupe doesn`t exthist or id is wrong");
            }

            return entity;
        }

        public async Task<string> DeleteGroupeAsync(int id)
        {
            var entity = await GetGroupeByIdAsync(id);

            var status  = _dbContext.Groupes.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return status.ToString();
        }

        public async Task<GroupeEntity> UpdateGroupeAsync(GroupeEntity entityUpdate)
        {
            var entity = await GetGroupeByIdAsync(entityUpdate.Id);

            entity.Name = entityUpdate.Name;
            entity.UpdatedAt = DateTime.UtcNow;
            
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<GroupeEntity>> GetGroupesAsync() =>
            await _dbContext.Groupes.ToListAsync();
        
    }
}
