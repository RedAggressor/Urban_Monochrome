namespace Catalog.Host.Models.Responses
{
    public class ItemsByPageResponse<T>
    {
        public long TotalCountItem { get; set; }
        public IEnumerable<T>? Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
