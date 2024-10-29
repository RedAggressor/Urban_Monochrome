using Infrastucture.Services.Abstractions;
using Newtonsoft.Json;

namespace Infrastucture.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T data) =>
            JsonConvert.SerializeObject(data);

        public T Deserialize<T>(string value) => 
            JsonConvert.DeserializeObject<T>(value)!;

    }
}
