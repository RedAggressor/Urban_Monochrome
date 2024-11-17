using Newtonsoft.Json;

namespace Nitifacation.Host.Models
{
    public class RequestBodyAccess
    {
        [JsonProperty("grant_type")]
        public string GrandType { get; set; } = "client_credentials";
        [JsonProperty("client_id")]
        public string ClientId { get; set; } = null!;
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; } = null!; 
    }
}
