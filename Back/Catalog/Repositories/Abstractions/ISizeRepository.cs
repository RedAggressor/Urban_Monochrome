using Catalog.Host.Data.Entities;
using System.Threading.Tasks;

namespace Catalog.Host.Repositories.Abstractions
{
    public interface ISizeRepository
    {
        Task<int> AddSizeAsync(string name);
        Task<SizeEntity> GetSizeByIdAsync(int id);
        Task<string> DeleteSizeByIdAsync(int id);
        Task<SizeEntity> UpdateSizeAsync(int id, string name);
        Task<ICollection<SizeEntity>> GetSizesAsync();
    }
}
