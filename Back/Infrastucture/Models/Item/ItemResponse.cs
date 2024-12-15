using System.Text.Json.Serialization;

namespace Infrastucture.Models.Item
{
    public class ItemResponse : Item
    {
       
        [JsonPropertyName("CategoryId")]
        public int TypeId { get; set; }
        [JsonPropertyName("Category")]
        public TypeDto? Type { get; set; }
        [JsonPropertyName("CollectionId")]
        public int GroupeId { get; set; }
        [JsonPropertyName("Collection")]
        public GroupeDto? Groupe { get; set; }
        [JsonPropertyName("Gender")]
        public string? Sex { get; set; }       
    }
}
