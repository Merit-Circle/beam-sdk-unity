using System;
using BeamPlayerClient.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Beam.Models
{
    public class BeamSessionRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
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

    public static class BeamSessionRequestStatusExtensions
    {
        public static BeamSessionRequestStatus ToBeamSessionRequestStatus(this GetSessionRequestResponse.StatusEnum statusEnum)
        {
            switch (statusEnum)
            {
                case GetSessionRequestResponse.StatusEnum.Pending:
                    return BeamSessionRequestStatus.Pending;
                case GetSessionRequestResponse.StatusEnum.Accepted:
                    return BeamSessionRequestStatus.Accepted;
                case GetSessionRequestResponse.StatusEnum.Error:
                    return BeamSessionRequestStatus.Error;
                default:
                    throw new ArgumentOutOfRangeException(nameof(statusEnum), statusEnum, null);
            }
        }
    }
}