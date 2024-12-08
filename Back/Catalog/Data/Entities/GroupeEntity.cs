using System.Data;

namespace Catalog.Host.Data.Entities
{
    public class GroupeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ItemEntity> Items { get; set; } = new List<ItemEntity>();
    }
}
