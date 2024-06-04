using System;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Beam.Api
{
    public class BeamHttpResult<T> where T : class
    {
        /// <summary>
        /// Http status code of a response
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Deserialized model from successful response
        /// </summary>
        public T Result { get; set; }
        
        /// <summary>
        /// Beam API specific error, if included
        /// </summary>
        public string Error { get; set; }
        
        /// <summary>
        /// Beam API specific error message, if included
        /// </summary>
        public string ErrorMessage { get; set; }

        public bool Success => StatusCode is >= 200 and < 300;

        public BeamHttpResult(UnityWebRequest request)
        {
            StatusCode = (int)request.responseCode;

            if (request.result != UnityWebRequest.Result.Success)
            {
                try
                {
                    var errorModel = JsonConvert.DeserializeObject<BeamApiError>(request.downloadHandler.text);
                    if (errorModel is { Message: not null, TraceId: not null })
                    {
                        Error = errorModel.Error;
                        ErrorMessage =
                            $"{request.method} - {request.url} received {StatusCode} with error: {errorModel.Error} error: {errorModel.Message}, traceId: {errorModel.TraceId}";
                    }
                    else
                    {
                        Error = "unknown";
                        ErrorMessage = $"{request.method} - {request.url} received {StatusCode} with content: {request.downloadHandler.text}";
                    }
                }
                catch
                {
                    Error = "unknown";
                    ErrorMessage = $"{request.method} - {request.url} received {StatusCode} with content: {request.downloadHandler.text}";
                }
                return;
            }

            try
            {
                Result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
            }
            catch (Exception e)
            {
                ErrorMessage = $"Encountered exception when deserializing response: {e.Message}";
            }
        }
    }
}