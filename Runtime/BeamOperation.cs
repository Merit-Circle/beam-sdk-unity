using System;
using Newtonsoft.Json.Converters;

namespace Beam
{
    public class BeamOperation
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))] 
        public BeamOperationStatus Status;
        public string Id;
        public string GameId;
        public string Error;
        public DateTimeOffset CreatedAt;
        public DateTimeOffset? UpdatedAt;
    }
}