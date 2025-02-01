using Microsoft.AspNetCore.Http;

namespace Infrastucture.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserClaimValueByType(this HttpContext context, string typeName)
        {
            var value = context.User.Claims.FirstOrDefault(f => f.Type == typeName)?.Value!;

            if(value != null)
            {
                return value;
            }

            throw new Exception("Claim isn`t exsist or something is wrong");
        }
    }
}
