using System;
using System.Collections;
using Beam.Models;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Beam.Api
{
    internal class BeamApi
    {
        private string m_ApiBaseUrl = string.Empty;
        private string m_BeamPublishableGameKey = string.Empty;

        public void SetBaseUrl(string baseUrl)
        {
            m_ApiBaseUrl = baseUrl;
        }

        public void SetApiKey(string apiKey)
        {
            m_BeamPublishableGameKey = apiKey;
        }

        public IEnumerator GetActiveSessionInfo(
            string entityId,
            string accountAddress,
            Action<BeamHttpResult<BeamSession>> callback,
            int chainId = Constants.DefaultChainId)
        {
            EnsureApiIsConfigured();
            var path = $"{m_ApiBaseUrl}/v1/self-custody/sessions/users/{entityId}/{accountAddress}/active?chainId={chainId}";
            var request = UnityWebRequest.Get(path);
            request.SetRequestHeader(Constants.BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();

            var result = new BeamHttpResult<BeamSession>(request);
            callback.Invoke(result);
        }

        public IEnumerator CreateSessionRequest(string entityId, int chainId, string address, Action<BeamHttpResult<BeamSessionRequest>> result)
        {
            EnsureApiIsConfigured();
            var bodyJson = JsonConvert.SerializeObject(new
            {
                address,
                chainId
            });

            var path = $"{m_ApiBaseUrl}/v1/self-custody/sessions/users/{entityId}/request";
            var request = UnityWebRequest.Post(path, bodyJson, "application/json");
            request.SetRequestHeader(Constants.BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();

            result.Invoke(new BeamHttpResult<BeamSessionRequest>(request));
        }

        
        public IEnumerator GetOperationById(string opId, Action<BeamHttpResult<BeamOperation>> result)
        {
            EnsureApiIsConfigured();
            var path = $"{m_ApiBaseUrl}/v1/self-custody/operation/{opId}";
            var request = UnityWebRequest.Get(path);
            request.SetRequestHeader(Constants.BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();

            result.Invoke(new BeamHttpResult<BeamOperation>(request));
        }
        
        public IEnumerator ConfirmOperation(
            string operationId,
            BeamOperationConfirmation beamOperationConfirmation,
            Action<BeamHttpResult<BeamOperation>> result)
        {
            EnsureApiIsConfigured();
            var bodyJson = JsonConvert.SerializeObject(beamOperationConfirmation);
            var request = UnityWebRequest.Post($"{m_ApiBaseUrl}/v1/self-custody/operation/{operationId}", bodyJson,
                "application/json");
            request.method = "PATCH";
            request.SetRequestHeader(Constants.BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();
            
            result.Invoke(new BeamHttpResult<BeamOperation>(request));
        }
        
        public IEnumerator GetSessionRequestById(
            string requestId,
            Action<BeamHttpResult<BeamSessionRequest>> result)
        {
            EnsureApiIsConfigured();
            var request =
                UnityWebRequest.Get($"{m_ApiBaseUrl}/v1/self-custody/sessions/request/{requestId}");
            request.SetRequestHeader(Constants.BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();
            
            result.Invoke(new BeamHttpResult<BeamSessionRequest>(request));
        }
        
        private void EnsureApiIsConfigured()
        {
            if (string.IsNullOrEmpty(m_BeamPublishableGameKey))
            {
                throw new ArgumentNullException("beamApiKey", "Set Beam Api Key first!");
            }
            
            if (string.IsNullOrEmpty(m_ApiBaseUrl))
            {
                throw new ArgumentNullException("beamApiBaseUrl", "Set Beam API Base Url first!");
            }
        }
    }
}