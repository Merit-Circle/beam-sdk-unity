using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Beam.Models
{
    public class BeamOperationConfirmation
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationStatus Status { get; set; }

        [JsonProperty("transactions")]
        public ICollection<TransactionConfirmation> Transactions { get; set; } = new List<TransactionConfirmation>();

        [JsonProperty("error")] public string Error { get; set; }
        [JsonProperty("entityId")] public string EntityId { get; set; }
        [JsonProperty("gameId")] public string GameId { get; set; }
    }

    public class TransactionConfirmation
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("signature")] public string Signature { get; set; }
    }
}