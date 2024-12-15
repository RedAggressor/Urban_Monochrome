namespace Catalog.Host.Data.Entities
{
    public class ColorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;       
        public ICollection<UniqueItemEntity> UniqueItems { get; set; } = null!;
    }
}
