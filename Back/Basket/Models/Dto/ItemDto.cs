using Basket.Host.Enums;

namespace Basket.Host.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public double Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public TypeDto? Type { get; set; } = null;
        public int NestedTypeId { get; set; } = 0;
        public NestedTypeDto? NestedType { get; set; } = null;
        public double Size { get; set; } = 0;
        public string? Color { get; set; } = null;
        public SexType Sex { get; set; } = SexType.None;
        public string? ImageUrl { get; set; } = null;
    }
}
