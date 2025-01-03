using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IGroupeRepository
    {
        Task<int> AddGroupeAsync(string name);
        Task<GroupeEntity> GetGroupeByIdAsync(int id);
        Task<string> DeleteGroupeAsync(int id);
        Task<GroupeEntity> UpdateGroupeAsync(GroupeEntity entityUpdate);
        Task<ICollection<GroupeEntity>> GetGroupesAsync();
    }
}
