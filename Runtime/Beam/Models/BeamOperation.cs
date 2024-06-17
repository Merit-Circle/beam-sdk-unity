using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Beam.Models
{
    internal class BeamOperation
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public BeamOperationStatus Status { get; set; }

        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("userId")] public string UserId { get; set; }
        [JsonProperty("gameId")] public string GameId { get; set; }
        [JsonProperty("createdAt")] public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty("updatedAt")] public DateTimeOffset? UpdatedAt { get; set; }
        [JsonProperty("chainId")] public int? ChainId { get; set; }
        [JsonProperty("transactions")] public IEnumerable<BeamOperationTransaction> Transactions { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }
}