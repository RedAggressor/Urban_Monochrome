using Catalog.Host.Data;
using Catalog.Host.enums;
using Catalog.Host.Extensions;
using Catalog.Host.Mapping;
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

        public async Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest infoRequest)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var filter = new FilterItems
                {
                    PageIndex = infoRequest.PageIndex,
                    PageSize = infoRequest.PageSize,
                    ColorFilterById = infoRequest.ColorFiltersById,
                    SexFilters = infoRequest.SexFilters.ConvertEnum<SexType>(),
                    GroupeFiltersById = infoRequest.GroupeFiltersById,
                    SortBy = infoRequest.SortBy.ConvertEnum<SortByType>(),
                    HotItems = infoRequest.HotItems,
                    SizeFiltersById = infoRequest.SizeFiltersById,
                    TypeFiltersById = infoRequest.TypeFiltersById,
                    PriceFilter = infoRequest.PriceFilter                     
                };

                var response = await _itemRepository.GetItemsByPageAsync(filter);

                return new ItemsByPageResponse<ItemDto>()
                {
                    TotalCountItem = response.TotalCountItem,
                    Data = response.Data.Select(item => item.MapToItemDto())!,
                    PageIndex = infoRequest.PageIndex,
                    PageSize = infoRequest.PageSize
                };
            });            
        }

        public async Task<DataResponse<IEnumerable<ItemDto>>> GetItdeByNameAsync(DataRequest<string> dataRequest)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var searchValue = dataRequest.Data!;
                var response = await _itemRepository.GetItemsByNameAsync(searchValue);

                return new DataResponse<IEnumerable<ItemDto>>()
                {
                    Data = response?.Select(s=>s.MapToItemDto()).ToList()!
                };
            });            
        }

        public async Task<DataResponse<IEnumerable<ItemDto>>> GetItemsByIdAsync(DataRequest<List<int>> dataRequest)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var listItemId = dataRequest.Data!;
                var result = await _itemRepository.GetItemsByIdAsync(listItemId);

                return new DataResponse<IEnumerable<ItemDto>>()
                {
                    Data = result?.Select(s => s.MapToItemDto()).ToList()!
                };
            });
            
        }
    }
}
