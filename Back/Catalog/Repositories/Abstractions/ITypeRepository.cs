using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface ITypeRepository
    {
        Task<int> AddTypeAsync(string name);
        Task<TypeEntity> GetTypeById(int id);
        Task<string> DeleteTypeByIdAsync(int id);
        Task<TypeEntity> UpdateTypeAsync(int id, string name);
        Task<ICollection<TypeEntity>> GetTypesAsync();
    }
}
