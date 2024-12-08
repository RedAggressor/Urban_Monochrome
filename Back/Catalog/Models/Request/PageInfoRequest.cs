using Catalog.Host.Models.Dto;

namespace Catalog.Host.Models.Request
{
    public class PageInfoRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; } = null;
        [JsonPropertyName("CategoryFilters")]
        public List<int>? TypeFiltersById { get; set; } = null;
        [JsonPropertyName("CollectionFilters")]
        public List<int>? GroupeFiltersById { get; set; } = null;
        public List<int>? SizeFiltersById { get; set; } = null;
        [JsonPropertyName("GenderFilters")]
        public string? SexFilters { get; set; } = null;
        public int? ColorFiltersById { get; set; }
        public HotItemsFilter? HotItems { get; set; } = null;
        public PriceFilter? PriceFilter { get; set; } = null;
    }
}
