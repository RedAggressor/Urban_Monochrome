using Order.Host.Data.Entities;
using Order.Host.Models.Dto;

namespace Order.Host.Extenctions
{
    public static class OrderMapExtemtion
    {
        public static OrderDto? MapToOrderDto(this OrderEntity? orderEntity)
        {
            return orderEntity is null ? null : new OrderDto()
            {
                Id = orderEntity.Id,
                UserId = orderEntity.UserId,
                OrderStatus = orderEntity.StatusOrder,
                OrderItems = orderEntity.OrderItems.Select(orderItem => orderItem.MapToOrderItemDto()).ToList()!,
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
                OrderItems = orderDto.OrderItems.Select(orderItem => orderItem.MapToOrderItemEntity()).ToList()!,                 
            };
        }
    }
}
