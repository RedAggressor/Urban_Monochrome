using System.Text;

namespace Catalog.Host.Extensions
{
    public static class ConvertCostumeEnum
    {
        public static T ConvertEnum<T>(this string? value) 
            where T : struct, Enum
        {
            if(string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            var builder = new StringBuilder(value.ToLower());
            builder[0] = char.ToUpper(builder[0]);

            return (value is not null && Enum.TryParse(builder.ToString(), true, out T result)) ?
                result :
                default(T);            
        }
    }
}
