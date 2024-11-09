using Catalog.Host.Data;
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

        public async Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest infoRequest)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByPageAsync(
                    infoRequest.PageIndex,
                    infoRequest.PageSize,
                    infoRequest.OrderType,
                    infoRequest.typeFilters,
                    infoRequest.nestedTypeFilters);

                return new ItemsByPageResponse<ItemDto>()
                {
                    TotalCountItem = response.TotalCountItem,
                    Data = response.Data.Select(item => item.MapToItemDto())!,
                    PageIndex = infoRequest.PageIndex,
                    PageSize = infoRequest.PageSize
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItdeByNameAsync(DataRequest<string> dataRequest)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByNameAsync(dataRequest.Data!);

                return new DataResponse<ItemDto>()
                {
                    Data = response.MapToItemDto()
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
                    Data = result.Select(s => s.MapToItemDto()).ToList()!
                };
            });
            
        }
    }
}
