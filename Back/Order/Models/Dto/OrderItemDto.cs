namespace Order.Host.Models.Dto
{
    public class OrderItemDto
    {      
        public int Count { get; set; }
        public UniqueItemResponse? Item { get; set; }
        public double Price { get; set; }
    }
}
