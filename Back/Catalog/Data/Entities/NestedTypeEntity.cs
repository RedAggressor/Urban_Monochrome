namespace Catalog.Host.Data.Entities
{
    public class NestedTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TypeId { get; set; }
        public TypeEntity Type { get; set; } = null!;
        public ICollection<ItemEntity> Items { get; set; } = new List<ItemEntity>();
    }
}
