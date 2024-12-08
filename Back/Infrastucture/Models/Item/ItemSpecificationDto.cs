namespace Infrastucture.Models.Item
{
    public class ItemSpecificationDto
    {
        public int Id { get; set; }
        public ColorDto? Color { get; set; }
        public SizeDto? Size { get; set; }
        public int Quantity { get; set; }
    }
}
