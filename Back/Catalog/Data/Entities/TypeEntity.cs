namespace Catalog.Host.Data.Entities
{
    public class TypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;       
        public List<ItemEntity> Items { get; set; } = new List<ItemEntity>();
        public List<NestedTypeEntity> NestedTypes { get; set; } = new List<NestedTypeEntity>();
    }
}
