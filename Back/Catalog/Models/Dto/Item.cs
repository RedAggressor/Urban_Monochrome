namespace Catalog.Host.Models.Dto
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        [JsonPropertyName("CategoryId")]
        public int TypeId { get; set; }
        [JsonPropertyName("Category")]
        public TypeDto? Type { get; set; }
        [JsonPropertyName("CollectionId")]
        public int GroupeId { get; set; }
        [JsonPropertyName("Collection")]
        public GroupeDto? Groupe { get; set; }
        [JsonPropertyName("ItemSpecification")]
        public string? Sex { get; set; }
        public string? ImageUrl { get; set; }
        public double Discount { get; set; }
    }
}
