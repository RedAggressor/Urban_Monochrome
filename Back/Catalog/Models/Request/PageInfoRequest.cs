using Catalog.Host.enums;

namespace Catalog.Host.Models.Request
{
    public class PageInfoRequest
    {
        public int PageIndex { get; set; } 
        public int PageSize { get; set; }
        public OrderType OrderType { get; set; } = OrderType.None;
    }
}
