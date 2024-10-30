namespace Basket.Host.Models.Requests
{
    public class DataRequest<T>
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
