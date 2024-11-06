namespace Order.Host.Data.Entities
{
    public class OrderEntity
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? StatusOrder { get; set; } 
        public ICollection<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
