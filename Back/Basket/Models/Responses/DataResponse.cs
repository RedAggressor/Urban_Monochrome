namespace Basket.Host.Models.Responses
{
    public class DataResponse<T>: BaseResponse
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
