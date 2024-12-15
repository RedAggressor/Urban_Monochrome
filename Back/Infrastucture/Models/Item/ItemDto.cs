using System.Text.Json.Serialization;

namespace Infrastucture.Models.Item
{
    public class ItemDto : ItemResponse
    {      
        [JsonPropertyName("ItemSpecification")]
        public List<UniqueItemDto>? UniqueItems { get; set; } = new List<UniqueItemDto>();        
    }
}
