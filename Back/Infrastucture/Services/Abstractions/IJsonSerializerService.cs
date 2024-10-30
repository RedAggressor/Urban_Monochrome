namespace Infrastucture.Services.Abstractions
{
    public interface IJsonSerializerService
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string value);
    }
}
