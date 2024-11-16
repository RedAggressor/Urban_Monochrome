namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        Task<BaseResponse> AddOrUpdateAsync<T>(string key, T value);
        Task<T> GetAsync<T>(string key);
    }
}
