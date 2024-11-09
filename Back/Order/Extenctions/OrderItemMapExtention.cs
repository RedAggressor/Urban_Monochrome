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
                Price = orderItem.Item!.Price,
            };
        }

        public static OrderItem? MapToOrderItemDto(this OrderItemEntity? orderItemEntity, ItemDto itemDto)
        {
            return orderItemEntity is null ? null : new OrderItem()
            {
                Id = orderItemEntity.Id,
                Count = orderItemEntity.Count,
                ItemId = orderItemEntity.ItemId,
                Price = orderItemEntity.Price,
                Item = itemDto
            };
        }
    }
}
