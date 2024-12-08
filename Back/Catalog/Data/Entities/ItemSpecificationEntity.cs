using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Catalog.Host.Data.Entities
{
    public class ItemSpecificationEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; } = null!;
        public int SizeId { get; set; }
        public SizeEntity Size { get; set; } = null!;
        public int ColorId { get; set; }
        public ColorEntity Color { get; set; } = null!;
        public int Quantity { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is ItemSpecificationEntity spec)
            {
                return Id == spec.Id &&
                    ItemId == spec.ItemId &&
                    SizeId == spec.SizeId &&
                    ColorId == spec.ColorId;
            }

            return false;            
        }
    }
}
