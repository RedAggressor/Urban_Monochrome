using Infrastucture.Enums;

namespace Infrastucture.Models
{
    public class BaseResponse
    {
        public string? ErrorMessage { get; set; } = null;
        public virtual ResponseCodeType ResponseCodeType => ErrorMessage is null ? ResponseCodeType.Success : ResponseCodeType.Failed;
    }
}
