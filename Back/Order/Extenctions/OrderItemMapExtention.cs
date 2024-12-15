using Order.Host.Data.Entities;
using Order.Host.Models;
using Order.Host.Models.Dto;

namespace Order.Host.Extenctions
{
    public static class OrderItemMapExtention
    {
        public static OrderItemEntity? MapToOrderItemEntity(this OrderItemDto? orderItem)
        {
            return orderItem is null ? null : new OrderItemEntity()
            {
                Count = orderItem.Count,
                ItemId = orderItem.Item!.Id,
                Price = orderItem.Item.Item!.Price,                 
            };
        }

        public static OrderItem? MapToOrderItemDto(this OrderItemEntity? orderItemEntity, UniqueItemRequest item)
        {
            return orderItemEntity is null ? null : new OrderItem()
            {
                Id = orderItemEntity.Id,
                Count = orderItemEntity.Count,
                ItemId = orderItemEntity.ItemId,
                Price = orderItemEntity.Price,                
                Item = new UniqueItemResponse
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Color = new ColorDto
                    {
                        Id = item.Color!.Id,
                        Name = item.Color.Name
                    },
                    Item = new ItemResponse
                    {
                        Id = item.Item!.Id,
                        Name = item.Item.Name,
                        Description = item.Item.Description,
                        Price = item.Item.Price,
                        Discount = item.Item.Discount,
                        Groupe = new GroupeDto
                        {
                            Id = item.Item.Groupe!.Id,
                            Name = item.Item.Groupe.Name
                        },
                        ImageUrl = item.Item.ImageUrl,
                        Sex = item.Item.Sex,
                        Type = new TypeDto
                        {
                            Id = item.Item.Type!.Id,
                            Name = item.Item.Type.Name,
                        }
                    },
                    Size = new SizeDto
                    {
                        Id = item.Size!.Id,
                        Name = item.Size.Name
                    }                     
                }
            };
        }
    }
}
