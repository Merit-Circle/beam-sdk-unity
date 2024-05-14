using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Beam
{
    public class BeamBrowserClient : MonoBehaviour
    {
        private const string BeamAPIKeyHeader = "x-api-key";

        // todo: export into settings or call to get
        private string m_BeamAuthUrl = string.Empty;
        private string m_ApiBaseUrl = string.Empty;
        private string m_BeamPublishableGameKey = string.Empty;

        public BeamBrowserClient()
        {
            SetEnvironment(BeamEnvironment.Testnet);
        }

        /// <summary>
        /// Sets Publishable Beam API key on the client. WARNING: Do not use keys other than Publishable, they're meant to be private, server-side only!
        /// </summary>
        /// <param name="beamApiKey">Publishable Beam API key</param>
        /// <returns>BeamBrowserClient</returns>
        public BeamBrowserClient SetPublishableBeamApiKey(string beamApiKey)
        {
            m_BeamPublishableGameKey = beamApiKey;
            return this;
        }

        /// <summary>
        /// Sets Environment on the client.
        /// </summary>
        /// <param name="environment">BeamEnvironment.Mainnet or BeamEnvironment.Testnet (defaults to Testnet)</param>
        /// <returns>BeamBrowserClient</returns>
        public BeamBrowserClient SetEnvironment(BeamEnvironment environment)
        {
            switch (environment)
            {
                case BeamEnvironment.Mainnet:
                    m_BeamAuthUrl = "https://identity.onbeam.com";
                    m_ApiBaseUrl = "https://api.onbeam.com";
                    break;
                default:
                case BeamEnvironment.Testnet:
                    m_BeamAuthUrl = "https://identity.preview.onbeam.com/"; // todo: replace with testnet
                    m_ApiBaseUrl = "https://api.preview.onbeam.com";    // todo: replace with testnet
                    break;
            }

            return this;
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a Session, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="callback">Callback to return a result of signin g</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 30</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator SignSession(string entityId, Action<BeamOperationResult> callback, int secondsTimeout = 120)
        {
            EnsureApiKeyIsSet();
            // url: http://localhost:3000/games/clvxqrso4000fbsialajt0m8c/session/create?entityId=Maciek

            // retrieve operation Id to pass further and track result
            string opId = null;
            string gameId = null;
            yield return StartCoroutine(GetOpId(operation =>
            {
                opId = operation.Id;
                gameId = operation.GameId;
            }, error =>
            {
                callback.Invoke(new BeamOperationResult
                {
                    Status = BeamOperationStatus.Error,
                    Error = error
                });
            }));
            
            if (opId == null || gameId == null)
            {
                yield break;
            }

            // open identity.onbeam.com, give it operation id
            Application.OpenURL($"{m_BeamAuthUrl}/games/{gameId}/session/create?entityId={entityId}&opId={opId}");
            
            // start polling for results of the operation
            yield return StartCoroutine(PollForOpResult(opId, callback, secondsTimeout));
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a transaction, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="transactionId">Transaction Id to sign by the User</param>
        /// <param name="callback">Callback to return a result of signin g</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 30</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator SignTransaction(string entityId, string transactionId, Action<BeamOperationResult> callback, int secondsTimeout = 120)
        {
            EnsureApiKeyIsSet();

            // retrieve operation Id to pass further and track result
            string opId = null;
            string gameId = null;
            yield return StartCoroutine(GetOpId(operation =>
            {
                opId = operation.Id;
                gameId = operation.GameId;
            }, error =>
            {
                callback.Invoke(new BeamOperationResult
                {
                    Status = BeamOperationStatus.Error,
                    Error = error
                });
            }));
            
            if (opId == null || gameId == null)
            {
                yield break;
            }

            // open identity.onbeam.com, give it operation id
            Application.OpenURL($"{m_BeamAuthUrl}/games/{gameId}/transaction/{transactionId}/confirm?entityId={entityId}&opId={opId}");
            
            // start polling for results of the operation
            yield return StartCoroutine(PollForOpResult(opId, callback, secondsTimeout));
        }

        private void EnsureApiKeyIsSet()
        {
            if (m_BeamPublishableGameKey == null)
            {
                throw new ArgumentNullException("beamApiKey", "Set Beam Api Key first!");
            }
        }

        private IEnumerator GetOpId(Action<BeamOperation> onOpIdCreated, Action<string> onError)
        {
            var request = UnityWebRequest.Post($"{m_ApiBaseUrl}/v1/operation", string.Empty, "application/json");
            request.SetRequestHeader(BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                BeamOperation model;
                try
                {
                    model = JsonConvert.DeserializeObject<BeamOperation>(request.downloadHandler.text);
                }
                catch
                {
                    onError.Invoke("Encountered an error when deserializing response from Operation creation");
                    yield break;
                }

                onOpIdCreated.Invoke(model);
                print($"Got opId: {model.Id}");
            }
            else
            {
                onError.Invoke(request.error);
            }
        }

        private IEnumerator PollForOpResult(string opId, Action<BeamOperationResult> callback,
            int secondsTimeout = 30)
        {
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                var path = $"{m_ApiBaseUrl}/v1/operation/{opId}";
                var request = UnityWebRequest.Get(path);
                request.SetRequestHeader(BeamAPIKeyHeader, m_BeamPublishableGameKey);
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var model = JsonConvert.DeserializeObject<BeamOperation>(request.downloadHandler.text);
                    print($"Got response: {model.Status.ToString()} {model.Error}");
                    if (model.Status != BeamOperationStatus.Pending)
                    {
                        callback.Invoke(new BeamOperationResult
                        {
                            Status = model.Status,
                            Error = model.Error
                        });

                        yield break;
                    }
                }

                yield return new WaitForSecondsRealtime(1);
            }

            callback.Invoke(new BeamOperationResult
            {
                Status = BeamOperationStatus.Error,
                Error = $"Reached {secondsTimeout}s timeout when getting operation result"
            });
        }
    }
}