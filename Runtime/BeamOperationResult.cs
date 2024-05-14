using Newtonsoft.Json.Converters;

namespace Beam
{
    public class BeamOperationResult
    {
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))] 
        public BeamOperationStatus Status;
        public string Error;
    }
}