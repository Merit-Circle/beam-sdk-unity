namespace Beam
{
    public static class Constants
    {
        public const int DefaultChainId = 13337;

        internal const string BeamAPIKeyHeader = "x-api-key";

        internal static class Storage
        {
            internal const string BeamSigningKey = "beam-session-signing-key";
            internal const string BeamSession = "beam-session-session-info";   
        }
    }
}