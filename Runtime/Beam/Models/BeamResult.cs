using BeamPlayerClient.Client;
using Newtonsoft.Json;

namespace Beam.Models
{
    public class BeamResult<T>
    {
        /// <summary>
        /// Overall result status
        /// </summary>
        public BeamResultType Status { get; set; }
        
        /// <summary>
        /// Summarized Error message
        /// </summary>
        public string Error { get; set; }
        
        /// <summary>
        /// Detailed Error model from the Beam API. Optional.
        /// </summary>
        public BeamApiError BeamApiError { get; set; }
        
        /// <summary>
        /// Actual result object
        /// </summary>
        public T Result { get; set; }

        public BeamResult()
        {
        }

        public BeamResult(ApiException e, string message)
        {
            Status = BeamResultType.Error;
            Error = message;

            HandleApiException(e);
        }
        public BeamResult(ApiException e)
        {
            Status = BeamResultType.Error;
            Error = e.Message;

            HandleApiException(e);
        }

        private void HandleApiException(ApiException e)
        {
            if (e.ErrorContent is string)
            {
                var text = e.ErrorContent as string;
                if (!string.IsNullOrEmpty(text))
                {
                    var deserializedError = JsonConvert.DeserializeObject<BeamApiError>(text);
                    BeamApiError = deserializedError;
                }
                
            }
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
        Error
    }
}