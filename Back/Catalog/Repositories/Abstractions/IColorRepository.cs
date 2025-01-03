using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface IColorRepository
    {
        Task<int> AddColorAsync(string name);
        Task<ColorEntity> GetColorByIdAsync(int id);
        Task<string> DeleteColorAsync(int id);
        Task<ColorEntity> UpdateColorAsync(int id, string newName);
        Task<ICollection<ColorEntity>> GetColorsAsync();
    }
}
