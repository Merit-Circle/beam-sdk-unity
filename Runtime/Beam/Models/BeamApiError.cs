namespace Beam.Models
{
    public class BeamApiError
    {
        public string Timestamp { get; set; }
        public int Status { get; set; }
        public string Error { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string TraceId { get; set; }
    }
}