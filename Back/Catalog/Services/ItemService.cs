using Catalog.Host.Data;
using Catalog.Host.Mapping;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class ItemService : BaseDataService<CatalogDbContext>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(
            IItemRepository itemRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> loggerBase
            )
            : base( dbContextWrapper, loggerBase)
        { 
            _itemRepository = itemRepository;
        }
        public async Task<DataResponse<int>> AddItemAsync(DataRequest<ItemDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var itemEntity = request!.Data!.MapToItemEntity();

                var response = await _itemRepository.AddItemAsync(itemEntity!);

                return new DataResponse<int>
                {
                    Data = response
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItemByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var id = request!.Data;
                var itenEntity = await _itemRepository.GetItemByIdAsync(id);

                var itemDto = itenEntity?.MapToItemDto();

                return new DataResponse<ItemDto> 
                { 
                    Data = itemDto
                };
            });
        }

        public async Task<DataResponse<string>> DeleteItemByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request!.Data;
                var response = await _itemRepository.DeleteItemByIdAsync(id);

                return new DataResponse<string> 
                { 
                    Data = response 
                };
            });
        }

        public async Task<DataResponse<ItemDto>> UpdateOrChangeItemAsync(DataRequest<ItemDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entityItemUpdate = request.Data!.MapToItemEntity();               

                var response = await _itemRepository.UpdateItemPropertyAsync(entityItemUpdate!);

                var dtoResponse = response?.MapToItemDto();

                return new DataResponse<ItemDto>()
                {
                    Data = dtoResponse
                };
            });
        }        
    }
}
