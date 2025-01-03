namespace Catalog.Host.Data.Entities
{
    public class TypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;       
        public ICollection<ItemEntity> Items { get; set; } = new List<ItemEntity>();
    }
}
