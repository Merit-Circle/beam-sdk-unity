using System;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Beam.Api
{
    public class BeamHttpResult<T> where T : class
    {
        public int StatusCode { get; set; }
        public T Result { get; set; }
        public string Error { get; set; }

        public bool Success => StatusCode is >= 200 and < 300;

        public BeamHttpResult(UnityWebRequest request)
        {
            StatusCode = (int)request.responseCode;

            if (request.result != UnityWebRequest.Result.Success)
            {
                Error = $"{request.method} - {request.url} received {StatusCode} with content: {request.downloadHandler.text}";
                return;
            }

            try
            {
                Result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
            }
            catch (Exception e)
            {
                Error = $"Encountered exception when deserializing response: {e.Message}";
            }
        }
    }
}