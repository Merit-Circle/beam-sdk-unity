using System;
using System.Collections.Generic;
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
        public IEnumerable<BeamOperationTransactionModel> Transactions { get; set; }
        public BeamGameModel Game { get; set; }
    }
}
