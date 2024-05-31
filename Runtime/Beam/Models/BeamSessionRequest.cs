using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Beam.Models
{
    public class BeamSessionRequest
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")] public BeamSessionRequestStatus Status { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("createdAt")] public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("updatedAt")] public DateTimeOffset? UpdatedAt { get; set; }
        [JsonProperty("chainId")] public int ChainId { get; set; }
        [JsonProperty("openfortId")] public string OpenfortId { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }
    
    public enum BeamSessionRequestStatus
    {
        Pending, Accepted, Error
    }
}