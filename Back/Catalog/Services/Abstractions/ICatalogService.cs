using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Abstractions
{
    public interface ICatalogService
    {
        Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest info);
        Task<DataResponse<IEnumerable<ItemDto>>> GetItdeByNameAsync(DataRequest<string> dataRequest);
        Task<DataResponse<IEnumerable<ItemDto>>> GetItemsByIdAsync(DataRequest<List<int>> dataRequest);
        Task<DataResponse<ExistFilters>> GetAllFilters();
        Task<DataResponse<IEnumerable<UniqueItemResponse>>> GetSpecificationsByIdAsync(DataRequest<List<int>> dataRequest);
    }
}
