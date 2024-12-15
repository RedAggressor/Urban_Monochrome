namespace Order.Host.Services.Interfaces
{
    public interface ICatalogHttpService
    {
        Task<ICollection<UniqueItemRequest>> GetSpecificationByIdAsync(List<int> listId);
    }
}
