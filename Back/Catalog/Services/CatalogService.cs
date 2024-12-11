using Catalog.Host.Data;
using Catalog.Host.enums;
using Catalog.Host.Extensions;
using Catalog.Host.Mapping;
using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;
using System.ComponentModel;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<CatalogDbContext>, ICatalogService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IGroupeRepository _gropueRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IColorRepository _colorRepository;
        public CatalogService(
            IItemRepository itemRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> loggerBase,
            IColorRepository colorRepository,
            ITypeRepository typeRepository,
            ISizeRepository sizeRepository,
            IGroupeRepository groupeRepository)
            : base(dbContextWrapper, loggerBase)
        {
            _itemRepository = itemRepository;
            _typeRepository = typeRepository;
            _sizeRepository = sizeRepository;
            _colorRepository = colorRepository;
            _gropueRepository = groupeRepository;
        }

        public async Task<ItemsByPageResponse<ItemDto>> GetOrderItemByPricePage(PageInfoRequest infoRequest)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var filter = new FilterItems
                {
                    PageIndex = infoRequest.PageIndex ?? 0,
                    PageSize = infoRequest.PageSize ?? 0,
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
                    PageIndex = infoRequest.PageIndex ?? 0,
                    PageSize = infoRequest.PageSize ?? 0
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
                    Data = response?.Select(s => s.MapToItemDto()).ToList()!
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

        public async Task<DataResponse<ExistFilters>> GetAllFilters()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var listGroupe = await _gropueRepository.GetGroupesAsync();
                var listType = await _typeRepository.GetTypesAsync();
                var listSize = await _sizeRepository.GetSizesAsync();
                var listColor = await _colorRepository.GetColorsAsync();
                var listSex = Enum.GetNames<SexType>().ToList();
                var listHotItems = GetHotItemPropertyToLis();

                return new DataResponse<ExistFilters>
                {
                    Data = new ExistFilters
                    {
                        Colors = listColor.Select(s => new ColorDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                        }),
                        Groups = listGroupe.Select(s => new GroupeDto
                        {
                            Id = s.Id,
                            Name = s.Name
                        }),
                        Sexs = listSex,
                        Sizes = listSize.Select(s => new SizeDto
                        {
                            Id = s.Id,
                            Name = s.Name
                        }),
                        Types = listType.Select(s => new TypeDto
                        {
                            Id = s.Id,
                            Name = s.Name
                        }),
                        HotItems = listHotItems
                    }
                };
            });
        }

        private List<string> GetHotItemPropertyToLis()
        {
            var properties = typeof(HotItemsFilter).GetProperties();
            var propertieslist = new List<string>();

            foreach (var property in properties)
            {
                propertieslist.Add(property.Name);
            }

            return propertieslist;
        }
    }
}
