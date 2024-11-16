using Catalog.Host.Data;
using Catalog.Host.Mapping;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Responses;
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
            ILogger<BaseDataService<CatalogDbContext>> loggerBase)
            : base( dbContextWrapper, loggerBase)
        { 
            _itemRepository = itemRepository;
        }
        public async Task<DataResponse<int>> AddItemAsync(ItemDto itemDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var itemEntity = itemDto!.MapToItemEntity();

                var response = await _itemRepository.AddItemAsync(itemEntity!);

                return new DataResponse<int>
                {
                    Data = response
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItemByIdAsync(int? id)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var itenEntity = await _itemRepository.GetItemByIdAsync(id);

                var itemDto = itenEntity.MapToItemDto();

                return new DataResponse<ItemDto> 
                { 
                    Data = itemDto
                };
            });
        }

        public async Task<DataResponse<string>> DeleteItemByIdAsync(int? id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var response = await _itemRepository.DeleteItemByIdAsync(id);

                return new DataResponse<string> 
                { 
                    Data = response 
                };
            });
        }

        public async Task<DataResponse<ItemDto>> UpdateOrChangeItemAsync(ItemDto itemDto)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entity = itemDto!.MapToItemEntity();
                var response = await _itemRepository.UpdateOrChangeItemAsync(entity!);
                var dto = response.MapToItemDto();

                return new DataResponse<ItemDto>()
                {
                    Data = dto
                };
            });
        }
    }
}
