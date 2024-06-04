using Newtonsoft.Json;

namespace Beam.Models
{
    public class BeamGame
    {
        [JsonProperty("pegiRating")] public string PegiRating { get; set; }
        [JsonProperty("pegiContent")] public string[] PegiContent { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("createdAt")] public string CreatedAt { get; set; }
        [JsonProperty("updatedAt")] public string UpdatedAt { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("coverImageUrl")] public string CoverImageUrl { get; set; }
        [JsonProperty("logoImageUrl")] public string LogoImageUrl { get; set; }
        [JsonProperty("backgroundImageUrl")] public string BackgroundImageUrl { get; set; }
        [JsonProperty("chainIds")] public int[] ChainIds { get; set; }
    }
}