namespace Catalog.Host.Models.Dto
{
    public class ItemSpecification
    {
        public int Id { get; set; }
        public ColorDto? Color { get; set; }
        public SizeDto? Size { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
    }
}
