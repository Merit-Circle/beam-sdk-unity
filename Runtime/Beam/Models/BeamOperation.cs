using System;
using System.Collections.Generic;
using Beam.Models;
using Newtonsoft.Json.Converters;

namespace Beam
{
    internal class BeamOperation
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationStatus Status;

        public string Id;
        public string Error;
        public string UserId;
        public string GameId;
        public DateTimeOffset CreatedAt;
        public DateTimeOffset? UpdatedAt;
        public int? ChainId;
        public IEnumerable<BeamOperationTransaction> Transactions { get; set; }
        public BeamGame Game { get; set; }
        public string Url { get; set; }
    }
}
