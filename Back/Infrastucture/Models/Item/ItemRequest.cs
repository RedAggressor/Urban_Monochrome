using Newtonsoft.Json;

namespace Infrastucture.Models.Item
{
    public class ItemRequest : Item
    {
        [JsonProperty("CategoryId")]
        public int TypeId { get; set; }
        [JsonProperty("Category")]
        public TypeDto? Type { get; set; }
        [JsonProperty("CollectionId")]
        public int GroupeId { get; set; }
        [JsonProperty("Collection")]
        public GroupeDto? Groupe { get; set; }
        [JsonProperty("Gender")]
        public string? Sex { get; set; }
    }
}
