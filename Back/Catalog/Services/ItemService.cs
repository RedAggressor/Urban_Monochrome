using Catalog.Host.Data;
using Catalog.Host.Mapping;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class ItemService : BaseDataService<ApplicationDbContext>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(
            IItemRepository itemRepository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> loggerBase)
            : base( dbContextWrapper, loggerBase)
        { 
            _itemRepository = itemRepository;
        }
        public async Task<DataResponse<int>> AddItemAsync(DataRequest<ItemDto> data)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var itemEntity = data.Data.MApToItemEntity();

                var response = await _itemRepository.AddItemAsync(itemEntity);

                return new DataResponse<int>
                {
                    Data = response
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItemByIdAsync(DataRequest<int> data)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var id = data.Data;

                var itenEntity = await _itemRepository.GetItemById(id);

                var itemDto = itenEntity.MapToItemDto();

                return new DataResponse<ItemDto> 
                { 
                    Data = itemDto
                };
            });
        }

        public async Task<DataResponse<string>> DeleteItemByIdAsync(DataRequest<int> data)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = data.Data;

                var response = await _itemRepository.DeleteItemById(id);

                return new DataResponse<string> 
                { 
                    Data = response 
                };
            });
        }
    }
}
