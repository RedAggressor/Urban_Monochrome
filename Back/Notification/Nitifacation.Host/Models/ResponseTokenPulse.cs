using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Nitifacation.Host.Models
{
    public class ResponseTokenPulse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = null!;
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; } = null!;
    }
}
