﻿namespace Catalog.Host.Data.Entities
{
    public class UniqueItemEntity
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
            if(obj is UniqueItemEntity spec)
            {
                return Id == spec.Id &&
                    ItemId == spec.ItemId &&
                    SizeId == spec.SizeId &&
                    ColorId == spec.ColorId &&
                    Quantity == spec.Quantity;
            }

            return false;            
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ItemId, SizeId, ColorId, Quantity);
        }
    }
}
