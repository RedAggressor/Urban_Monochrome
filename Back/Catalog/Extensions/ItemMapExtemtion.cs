using Catalog.Host.Data.Entities;
using Catalog.Host.enums;
using Catalog.Host.Models.Dto;

namespace Catalog.Host.Extensions
{
    public static class ItemMapExtemtion
    {
        public static Item? MapToItem(this ItemEntity itemEntity)
        {
            return itemEntity is null ? null : new Item
            {
                Id = itemEntity.Id,
                Name = itemEntity.Name,
                Description = itemEntity.Description,
                ImageUrl = itemEntity.ImageUrl,
                Price = itemEntity.Price,
                Sex = itemEntity.Sex.ToString(),
                TypeId = itemEntity.TypeId,
                Type = new TypeDto()
                {
                    Id = itemEntity.Type.Id,
                    Name = itemEntity.Type.Name
                },
                GroupeId = itemEntity.GroupeId,
                Groupe = new GroupeDto()
                {
                    Id = itemEntity.Groupe.Id,
                    Name = itemEntity.Groupe.Name
                },
                Discount = itemEntity.Discount
            };
        }

        public static ItemEntity? MapToEntity(this Item item) 
        {
            return item is null ? null : new ItemEntity
            {
                Id = item.Id,
                Name = item?.Name!,
                Description = item?.Description,
                ImageUrl = item?.ImageUrl!,
                Price = item!.Price,
                Sex = item!.Sex!.ConvertEnum<SexType>(),
                TypeId = item.TypeId,
                GroupeId = item.GroupeId,
            };
        }
    }
}
