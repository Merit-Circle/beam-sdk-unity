using Newtonsoft.Json.Converters;

namespace Beam
{
    public class BeamOperationTransactionModel
    {
        public string Id { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationTransactionType Type { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public BeamOperationTransactionStatus Status { get; set; }
        public string ExternalId { get; set; }
        public string Signature { get; set; }
        public string OperationId { get; set; }
        public dynamic Data { get; set; }
    }

    public enum BeamOperationTransactionType
    {
        OpenfortTransaction,
        MarketplaceOrder
    }

    public enum BeamOperationTransactionStatus
    {
        Pending,
        Executed,
        Error
    }
}