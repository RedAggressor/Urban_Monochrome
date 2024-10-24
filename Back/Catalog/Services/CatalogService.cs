﻿using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Mapping;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;


namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly IItemRepository _itemRepository;
        public CatalogService(
            IItemRepository itemRepository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> loggerBase)
            : base(dbContextWrapper, loggerBase) 
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest info)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByPageAsync(info.PageIndex, info.PageSize, info.OrderType);

                return new ItemsByPageResponse<ItemDto>()
                {
                    TotalCountItem = response.TotalCountItem,
                    Data = response.Data.Select(item => item.MapToItemDto()),
                    PageIndex = info.PageIndex,
                    PageSize = info.PageSize
                };
            });            
        }

        public async Task<DataResponse<ItemDto>> GetItdeByNameAsync(DataRequest<string> data)
        {
            return await ExecuteSafeAsync(async () => 
            {
                var response = await _itemRepository.GetItemsByName(data.Data!);

                return new DataResponse<ItemDto>()
                {
                    Data = response.MapToItemDto()
                };
            });            
        }
    }
}
