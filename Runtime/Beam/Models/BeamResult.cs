namespace Beam.Models
{
    public class BeamResult<T>
    {
        public BeamResultType Status { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }

        public BeamResult()
        {
        }

        public BeamResult(T result)
        {
            Status = BeamResultType.Success;
            Result = result;
        }

        public BeamResult(BeamResultType status, string error)
        {
            Status = status;
            Error = error;
        }
    }

    public enum BeamResultType
    {
        Pending,
        Success,
        Error,
        Timeout
    }
}