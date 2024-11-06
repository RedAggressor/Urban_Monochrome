using Infrastucture.Models;

namespace Order.Host.Dto.Responses
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
