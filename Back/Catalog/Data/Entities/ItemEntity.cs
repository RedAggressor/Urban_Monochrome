using Catalog.Host.enums;
using System.Drawing;

namespace Catalog.Host.Data.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price {  get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = null!;
        public double Size { get; set; }
        public string Color { get; set; } = null!;
        public SexType Sex { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
