using Nethereum.ABI.EIP712;

namespace Beam.Marketplace
{
    public class SeaportTypedData
    {
        public Types Types { get; set; }
        public string PrimaryType { get; set; }
        public Domain Domain { get; set; }
        public Message Message { get; set; }
    }

    public class Types
    {
        public MemberDescription[] OrderComponents { get; set; }
        public MemberDescription[] OfferItem { get; set; }
        public ConsiderationItem[] ConsiderationItem { get; set; }
    }

    public class ConsiderationItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Message
    {
        public string Offerer { get; set; }
        public Offer[] Offer { get; set; }
        public string OrderType { get; set; }
        public Consideration[] Consideration { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Zone { get; set; }
        public string ZoneHash { get; set; }
        public string Salt { get; set; }
        public string ConduitKey { get; set; }
        public string TotalOriginalConsiderationItems { get; set; }
        public string Counter { get; set; }
    }

    public class Offer
    {
        public string ItemType { get; set; }
        public string Token { get; set; }
        public string IdentifierOrCriteria { get; set; }
        public string StartAmount { get; set; }
        public string EndAmount { get; set; }
    }

    public class Consideration
    {
        public string ItemType { get; set; }
        public string Token { get; set; }
        public string IdentifierOrCriteria { get; set; }
        public string StartAmount { get; set; }
        public string EndAmount { get; set; }
        public string Recipient { get; set; }
    }
}