namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        Task AddOdUpdateAsync<T>(string key, T value);
        Task<T> GetAsync<T>(string key);
    }
}
