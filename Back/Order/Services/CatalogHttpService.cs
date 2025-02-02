using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class CatalogHttpService : ICatalogHttpService
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _urlCatalog; //= "http://www.urbanmonochrome.com:5000";
        private readonly string _blockUrl = "/api/v1/CatalogBFS/";
        public CatalogHttpService(IHttpClientService httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlCatalog = configuration["CATALOG_API_URL"]!;
        }

        public async Task<ICollection<UniqueItemRequest>> GetSpecificationByIdAsync(List<int> listId)
        {
            var request = new DataRequest<List<int>>()
            {
                Data = listId
            };

            var url = $"{_urlCatalog}{_blockUrl}GetSpecificationById";

            var response = await _httpClient
                .SendAsync<DataResponse<List<UniqueItemRequest>>, DataRequest<List<int>>>
                (url, HttpMethod.Post, request);

            if(response.ResponseCodeType is ResponseCodeType.Success)
            {
                return response.Data!;
            }            

            throw new Exception(response.ErrorMessage);
        }
    }
}
