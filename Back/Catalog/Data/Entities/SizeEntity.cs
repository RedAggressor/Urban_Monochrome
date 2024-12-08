namespace Catalog.Host.Data.Entities
{
    public class SizeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ItemSpecificationEntity> ItemSpecifications { get; set; } = null!;
    }
}
