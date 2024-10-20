using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IItemRepository _itemRepository;
        public CatalogService(IItemRepository itemRepository)
        { 
            _itemRepository = itemRepository;
        }

        public async Task<ItemsByPageResponse<ItemDto>> GetItemByPage(PageInfoRequest info)
        {
            var response = await _itemRepository.GetItemsByPageAsync(info.PageIndex, info.PageSize);

            return new ItemsByPageResponse<ItemDto>()
            {
                TotalCountItem = response.TotalCountItem,
                Data = response.Data.Select(s=> new ItemDto() 
                { 
                    Id = s.Id,
                    Name = s.ItemName
                }),
                PageIndex = info.PageIndex,
                PageSize = info.PageSize
            };
        }
    }
}
