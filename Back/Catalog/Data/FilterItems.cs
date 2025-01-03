using Catalog.Host.enums;
using Catalog.Host.Models.Dto;

namespace Catalog.Host.Data
{
    public class FilterItems
    {       
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SortByType SortBy { get; set; } = SortByType.None;        
        public List<int>? TypeFiltersById { get; set; } = null;       
        public List<int>? GroupeFiltersById { get; set; } = null;
        public List<int>? SizeFiltersById { get; set; } = null;
        public SexType SexFilters { get; set; } = SexType.None;
        public int? ColorFilterById { get; set; }
        public HotItemsFilter? HotItems { get; set; } = null;
        public PriceFilter? PriceFilter { get; set; } = null;
    }
}
