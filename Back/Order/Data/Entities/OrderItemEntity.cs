namespace Order.Host.Data.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ItemId { get; set; }
        public OrderEntity Order { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public double Price { get; set; }
    }
}
