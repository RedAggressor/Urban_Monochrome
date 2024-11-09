using Order.Host.Models.Dto;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class CatalogHttpService : ICatalogHttpService
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _urlCatalog;
        private readonly string _blockUrl = "/api/v1/CatalogBFS/";
        public CatalogHttpService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlCatalog = configuration["CATALOG_API_URL"];
        }

        public async Task<ICollection<ItemDto>> GetItemsByIdAsync(List<int> listId)
        {
            var request = new DataRequest<List<int>>()
            {
                Data = listId
            };

            var url = $"{_urlCatalog}{_blockUrl}GetItemsById";

            var response = await _httpClient
                .SendAsync<DataResponse<List<ItemDto>>, DataRequest<List<int>>>
                (url, HttpMethod.Post, request);

            if(response.ResponseCodeType is ResponseCodeType.Success)
            {
                return response.Data!;
            }            

            throw new Exception(response.ErrorMessage);
        }
    }
}
