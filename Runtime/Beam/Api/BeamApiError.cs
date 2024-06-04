using System;
using Newtonsoft.Json;

namespace Beam.Api
{
    public class BeamApiError
    {
        [JsonProperty("timestamp")] public DateTimeOffset Timestamp { get; set; }
        [JsonProperty("status")] public int Status { get; set; }
        [JsonProperty("error")] public string Error { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("traceId")] public string TraceId { get; set; }
    }
}