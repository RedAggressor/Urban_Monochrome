namespace Basket.Host.Models.Dto
{
    public class NestedTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TypeId { get; set; }
        public TypeDto Type { get; set; } = null!;
    }
}
