using Catalog.Host.enums;

namespace Catalog.Host.Data.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price {  get; set; }
        public int TypeId { get; set; }
        public TypeEntity Type { get; set; } = null!;
        public int GroupeId { get; set; }
        public GroupeEntity Groupe { get; set; } = null!;
        public ICollection<ItemSpecificationEntity> ItemSpecifications { get; set; } = new List<ItemSpecificationEntity>();
        public SexType Sex { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public double Discount { get; set; }
        public int Sold { get; set; }        
    }
}
