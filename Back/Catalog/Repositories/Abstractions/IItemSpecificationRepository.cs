using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemSpecificationRepository
    {
        Task<UniqueItemEntity> GetSpecificationByIdAsync(int id);
        Task<int> AddSpecificationAsync(UniqueItemEntity entity);
        Task<string> DeleteSpecificationAsync(int id);
        Task<UniqueItemEntity> UpdateSpecificationAsync(UniqueItemEntity specForUpdate);
        Task<ICollection<UniqueItemEntity>> GetSpecificationsByIdAsync(List<int> idList);
    }
}
