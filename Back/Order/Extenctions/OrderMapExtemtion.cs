using Order.Host.Data.Entities;
using Order.Host.Models.Dto;

namespace Order.Host.Extenctions
{
    public static class OrderMapExtemtion
    {
        public static OrderDto? MapToOrderDto(
            this OrderEntity? orderEntity,
            Dictionary<int, UniqueItemRequest> items)
        {
            return orderEntity is null ? null : new OrderDto()
            {
                Id = orderEntity.Id,
                UserId = orderEntity.UserId,
                OrderStatus = orderEntity.StatusOrder,
                OrderItems = orderEntity.OrderItems.Select(orderItem =>
                {
                    if(items.TryGetValue(orderItem.ItemId, out var uniqueItem))
                    {
                        return orderItem.MapToOrderItemDto(uniqueItem);
                    }
                    return null;
                }).Where(item => item is not null).ToList()!,
                CreatedAt = orderEntity.CreatedAt,
                UpdatedAt = orderEntity.UpdatedAt,
                PaymentStatus = orderEntity.PaymentStatus,
            };
        }

        public static OrderEntity? MapToOrderEntity(this OrderDto? orderDto)
        {
            return orderDto is null ? null : new OrderEntity()
            {
                UserId = orderDto.UserId,
                OrderItems = orderDto.OrderItems.Select(orderItem =>
                    orderItem.MapToOrderItemEntity()).ToList()!
            };
        }
    }
}
