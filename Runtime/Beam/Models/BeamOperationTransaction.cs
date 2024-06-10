using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Beam.Models
{
    public class BeamOperationTransaction
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("type")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationTransactionType Type { get; set; }

        [JsonProperty("status")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationTransactionStatus Status { get; set; }

        [JsonProperty("externalId")] public string ExternalId { get; set; }
        [JsonProperty("signature")] public string Signature { get; set; }
        [JsonProperty("operationId")] public string OperationId { get; set; }

        [JsonProperty("data")]
        [JsonConverter(typeof(BeamTransactionDataJsonConverter))]
        public string Data { get; set; }

        [JsonProperty("hash")] public string Hash { get; set; }
    }

    public enum BeamOperationTransactionType
    {
        OpenfortTransaction,
        OpenfortReservoirOrder
    }

    public enum BeamOperationTransactionStatus
    {
        Pending,
        Executed,
        Signed,
        Error
    }

    public class BeamTransactionDataJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is not string data)
            {
                return;
            }

            serializer.Serialize(writer, data);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            return token.ToString();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string) || objectType == typeof(object);
        }
    }
}