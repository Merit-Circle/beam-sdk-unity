using System;

namespace Beam.Api
{
    public class BeamApiError
    {
        public DateTimeOffset Timestamp { get; set; }

        public int Status { get; set; }
        
        public string Message { get; set; }

        public string TraceId { get; set; }
    }
}