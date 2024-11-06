namespace Order.Host.Models.Dto
{
    public class OrderItemDto
    {      
        public int Count { get; set; }
        public ItemDto? Item { get; set; }
        public decimal Price { get; set; }
    }
}
