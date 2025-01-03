namespace Catalog.Host.Models.Dto
{
    public class HotItemsFilter
    {
        public bool BestSeller { get; set; } = false;
        public bool New { get; set; } = false;
        public bool Sold { get; set; } = false;
    }
}
