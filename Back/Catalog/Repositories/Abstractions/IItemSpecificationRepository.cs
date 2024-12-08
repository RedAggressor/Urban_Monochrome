using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IItemSpecificationRepository
    {
        Task<ItemSpecificationEntity> GetSpecificationByIdAsync(int id);
        Task<int> AddSpecificationAsync(ItemSpecificationEntity entity);
        Task<string> DeleteSpecificationAsync(int id);
        Task<ItemSpecificationEntity> UpdateSpecificationAsync(ItemSpecificationEntity specForUpdate);
    }
}
