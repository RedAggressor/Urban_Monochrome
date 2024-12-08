using Catalog.Host.Data.Entities;
using Catalog.Host.enums;
using Catalog.Host.Extensions;

namespace Catalog.Host.Mapping
{
    public static class ItemDtoMapExtension
    {
        public static ItemDto? MapToItemDto(this ItemEntity itemEntity) =>
            itemEntity is null ? null : new ItemDto()
            {
                Id = itemEntity.Id,
                Name = itemEntity.Name,
                Description = itemEntity.Description,
                ImageUrl = itemEntity.ImageUrl,
                Price = itemEntity.Price,
                Sex = itemEntity.Sex.ToString(),
                ItemSpecifications = itemEntity.ItemSpecifications
                    .Select(s=> new ItemSpecificationDto() 
                        {
                            Id = s.Id,
                            Size = new SizeDto 
                            { 
                                Id = s.SizeId,
                                Name = s.Size.Name
                            },
                            Color = new ColorDto 
                            { 
                                Id = s.ColorId,
                                Name = s.Color.Name
                            },                            
                            Quantity = s.Quantity
                        })
                    .ToList(),
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

        public static ItemEntity? MapToItemEntity(this ItemDto itemDto) =>
            itemDto is null ? null : new ItemEntity()
            {
                Id = itemDto.Id,
                Name = itemDto?.Name!,                             
                Description = itemDto?.Description,
                ImageUrl = itemDto?.ImageUrl!,
                Price = itemDto!.Price,               
                Sex = itemDto!.Sex!.ConvertEnum<SexType>(),
                ItemSpecifications = itemDto.ItemSpecifications!
                    .Select(s => new ItemSpecificationEntity 
                        {
                            Id = s.Id,
                            SizeId = s.Size!.Id,
                            ItemId = itemDto.Id,
                            ColorId = s.Color!.Id,
                            Quantity = s.Quantity
                        })
                    .ToList(),
                TypeId = itemDto.TypeId,
                GroupeId = itemDto.GroupeId, 
            };
    }
}
