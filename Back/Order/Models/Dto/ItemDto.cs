namespace Order.Host.Models.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TypeId { get; set; }
        public TypeDto Type { get; set; } = null!;
        public int NestedTypeId {  get; set; }
        public NestedTypeDto NestedType { get; set; } = null!;
        public double Size { get; set; }
        public string? Color { get; set; }
        public string? Sex { get; set; }
        public string? ImageUrl { get; set; } 
    }
}
