using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dto;

namespace Catalog.Host.Extensions
{
    public static class SpecificationMapExtention
    {
        public static UniqueItemEntity? SpecificationMapToEntity(this UniqueItemResponse itemSpecification)
        {
            return itemSpecification is null ? null : new UniqueItemEntity
            { 
                Id = itemSpecification.Id,
                ColorId = itemSpecification.Color!.Id,
                Color = new ColorEntity
                {
                    Id = itemSpecification.Color.Id,
                    Name = itemSpecification.Color.Name!
                },
                SizeId = itemSpecification.Size!.Id,
                Size = new SizeEntity
                {
                    Id = itemSpecification.Size.Id,
                    Name = itemSpecification.Size.Name!
                },
                ItemId = itemSpecification.Item!.Id,
                Item = itemSpecification.Item.MapToEntity()!,
                Quantity = itemSpecification.Quantity
            };
        }

        public static UniqueItemResponse? SpecificationMapToDto(this UniqueItemEntity itemSpecificationEntity)
        {
            return itemSpecificationEntity is null ? null : new UniqueItemResponse
            {
                Id = itemSpecificationEntity.Id,
                Color = new ColorDto
                {
                    Id = itemSpecificationEntity.Color.Id,
                    Name = itemSpecificationEntity.Color.Name
                },
                Size = new SizeDto
                {
                    Id = itemSpecificationEntity.Size.Id,
                    Name = itemSpecificationEntity.Size.Name,
                },
                Item = itemSpecificationEntity.Item.MapToItem(),
                Quantity = itemSpecificationEntity.Quantity
            };
        }

    }
}
