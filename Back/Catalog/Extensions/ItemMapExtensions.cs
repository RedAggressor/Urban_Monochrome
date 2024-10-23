using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dto;

namespace Catalog.Host.Mapping
{
    public static class ItemMapExtensions
    {
        public static ItemDto? MapToItemDto(this ItemEntity itemEntity) =>
         itemEntity is null ? null : new ItemDto()
            {
                Id = itemEntity.Id,
                Name = itemEntity.Name,
                Color = itemEntity.Color,
                Description = itemEntity.Description,
                ImageUrl = itemEntity.ImageUrl,
                Price = itemEntity.Price,
                Quantity = itemEntity.Quantity,
                Sex = itemEntity.Sex,
                Size = itemEntity.Size,
                Type = itemEntity.Type,
            };

        public static ItemEntity? MApToItemEntity(this ItemDto itemDto) =>
            itemDto is null ? null : new ItemEntity()
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Color = itemDto.Color,
                Description = itemDto.Description,
                ImageUrl = itemDto.ImageUrl,
                Price = itemDto.Price,
                Quantity = itemDto.Quantity,
                Sex = itemDto.Sex,
                Size = itemDto.Size,
                Type = itemDto.Type,
            };
    }
}
