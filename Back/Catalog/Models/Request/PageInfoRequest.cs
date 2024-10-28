using Catalog.Host.enums;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Request
{
    public class PageInfoRequest
    {        
        public int PageIndex { get; set; } 
        public int PageSize { get; set; }
        public SortByType OrderType { get; set; } = SortByType.None;
        public List<string> typeFilters { get; set; } = null!;
        public List<string>? nestedTypeFilters { get; set; } = null;
    }
}
