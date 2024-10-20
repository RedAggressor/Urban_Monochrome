namespace Catalog.Host.Data
{
    public class PaginatedItems <T>
    {
        public long TotalCountItem { get; set; }
        public IEnumerable<T> Data { get; set;} = null!;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
