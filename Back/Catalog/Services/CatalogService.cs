using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Mapping;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;


namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<CatalogDbContext>, ICatalogService
    {
        private readonly IItemRepository _itemRepository;
        public CatalogService(
            IItemRepository itemRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> loggerBase)
            : base(dbContextWrapper, loggerBase) 
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest info)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByPageAsync(info.PageIndex, info.PageSize, info.OrderType, info.typeFilters, info.nestedTypeFilters);

                return new ItemsByPageResponse<ItemDto>()
                {
                    TotalCountItem = response.TotalCountItem,
                    Data = response.Data.Select(item => item.MapToItemDto())!,
                    PageIndex = info.PageIndex,
                    PageSize = info.PageSize
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItdeByNameAsync(DataRequest<string> data)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByNameAsync(data.Data!);

                return new DataResponse<ItemDto>()
                {
                    Data = response.MapToItemDto()
                };
            });            
        }
    }
}
