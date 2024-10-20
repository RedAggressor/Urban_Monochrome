using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Abstractions
{
    public interface ICatalogService
    {
        Task<ItemsByPageResponse<ItemDto>> GetItemByPage(PageInfoRequest info);
    }
}
