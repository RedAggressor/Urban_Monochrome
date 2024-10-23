using Infrastucture.Models;

namespace Catalog.Host.Models.Responses
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
