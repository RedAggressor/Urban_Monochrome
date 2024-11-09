using Order.Host.Models.Dto;

namespace Order.Host.Services.Interfaces
{
    public interface ICatalogHttpService
    {
        Task<ICollection<ItemDto>> GetItemsByIdAsync(List<int> listId);
    }
}
