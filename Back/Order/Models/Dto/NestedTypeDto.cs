using Order.Host.Data.Entities;

namespace Order.Host.Models.Dto
{
    public class NestedTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TypeId { get; set; }
        public TypeDto Type { get; set; } = null!;
    }
}
