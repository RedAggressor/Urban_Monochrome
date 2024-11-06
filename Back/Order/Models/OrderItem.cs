using Order.Host.Models.Dto;

namespace Order.Host.Models
{
    public class OrderItem : OrderItemDto
    {
        public int? Id { get; set; }
        public int ItemId { get; set; }

    }
}
