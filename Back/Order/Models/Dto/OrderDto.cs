namespace Order.Host.Models.Dto
{
    public class OrderDto
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? PaymentStatus {  get; set; }
    }
}
