namespace Catalog.Host.Models.Dto
{
    public class ExistFilters
    {
        [JsonPropertyName("Gender")]
        public IEnumerable<string> Sexs { get; set; } = new List<string>();
        [JsonPropertyName("Categories")]
        public IEnumerable<TypeDto> Types { get; set; } = new List<TypeDto>();
        [JsonPropertyName("Colections")]
        public IEnumerable<GroupeDto> Groups { get; set; } = new List<GroupeDto>();
        [JsonPropertyName("HotItems")]
        public IEnumerable<string> HotItems { get; set; } = new List<string>();
        [JsonPropertyName("Size")]
        public IEnumerable<SizeDto> Sizes { get; set; } = new List<SizeDto>();
        
        [JsonPropertyName("Color")]
        public IEnumerable<ColorDto> Colors { get; set; } = new List<ColorDto>();
        
    }
}
