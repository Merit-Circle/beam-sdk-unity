using System;
using System.Collections;
using System.Linq;
using Nethereum.ABI.EIP712;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Beam
{
    public class BeamClient : MonoBehaviour
    {
        private const string BeamAPIKeyHeader = "x-api-key";
        private const string BeamSigningKeyStorageKey = "beam-session-signing-key";
        private const string BeamSessionStorageKey = "beam-session-session-info";

        // todo: export into settings or call to get
        private string m_BeamAuthUrl = string.Empty;
        private string m_ApiBaseUrl = string.Empty;
        private string m_BeamGameId = string.Empty;
        private string m_BeamPublishableGameKey = string.Empty;
        private IStorage m_Storage;

        public BeamClient()
        {
            SetEnvironment(BeamEnvironment.Testnet);
            m_Storage = new PlayerPrefsStorage();
        }

        /// <summary>
        /// Sets Publishable Beam API key on the client. WARNING: Do not use keys other than Publishable, they're meant to be private, server-side only!
        /// </summary>
        /// <param name="publishableApiKey">Publishable Beam API key</param>
        /// <returns>BeamBrowserClient</returns>
        public BeamClient SetBeamApiGame(string gameId, string publishableApiKey)
        {
            m_BeamGameId = gameId;
            m_BeamPublishableGameKey = publishableApiKey;
            return this;
        }

        /// <summary>
        /// Sets Environment on the client.
        /// </summary>
        /// <param name="environment">BeamEnvironment.Mainnet or BeamEnvironment.Testnet (defaults to Testnet)</param>
        /// <returns>BeamBrowserClient</returns>
        public BeamClient SetEnvironment(BeamEnvironment environment)
        {
            switch (environment)
            {
                case BeamEnvironment.Mainnet:
                    m_BeamAuthUrl = "https://identity.onbeam.com";
                    m_ApiBaseUrl = "https://api.onbeam.com";
                    break;
                default:
                case BeamEnvironment.Testnet:
                    m_BeamAuthUrl = "https://identity.preview.onbeam.com/"; // todo: change back to testnet
                    m_ApiBaseUrl = "https://api.preview.onbeam.com";
                    break;
            }

            return this;
        }

        /// <summary>
        /// Sets custom storage for Session related information. Defaults to <see cref="PlayerPrefsStorage"/>.
        /// </summary>
        /// <param name="storage">Storage that implements IStorage</param>
        /// <returns>BeamBrowserClient</returns>
        public BeamClient SetCustomStorage(IStorage storage)
        {
            m_Storage = storage;
            return this;
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a Session, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="callback">Callback to return a result of signin g</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 30</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator CreateSession(
            string entityId,
            int chainId,
            Action<BeamResult<BeamSessionStatus>> callback,
            int secondsTimeout = 120)
        {
            EnsureApiKeyIsSet();

            var hasActiveSession = false;
            yield return StartCoroutine(GetActiveSessionKeyPair(entityId, callback =>
            {
                if (callback != null)
                {
                    hasActiveSession = true;
                }
            }));

            if (hasActiveSession)
            {
                callback.Invoke(new BeamResult<BeamSessionStatus>(BeamResultType.Error, "Already has an active session"));
                yield break;
            }

            // retrieve operation Id to pass further and track result
            string opId = null;
            string gameId = null;
            yield return StartCoroutine(CreateOperation(entityId, chainId, operation =>
            {
                opId = operation.Id;
                gameId = operation.GameId;
            }, error => { callback.Invoke(new BeamResult<BeamSessionStatus>(status: BeamResultType.Error, error: error)); }));

            if (opId == null || gameId == null)
            {
                yield break;
            }

            // key pair used to sign the session, we pass public part to identity.onbeam.com
            var keyPair = GetOrCreateSigningKeyPair();

            // open identity.onbeam.com, give it operation id
            Application.OpenURL(
                $"{m_BeamAuthUrl}/games/{gameId}/session/create?entityId={entityId}&opId={opId}&address={keyPair.Account.Address}");

            // start polling for results of the operation
            yield return StartCoroutine(PollForOperationResult(opId, operationResult =>
                {
                    var beamResult = new BeamResult<BeamSessionStatus>();
                    switch (operationResult.Status)
                    {
                        case BeamOperationStatus.Pending:
                            beamResult.Status = BeamResultType.Pending;
                            break;
                        case BeamOperationStatus.Approved:
                            beamResult.Result = BeamSessionStatus.Signed;
                            beamResult.Status = BeamResultType.Success;
                            break;
                        case BeamOperationStatus.Rejected:
                            beamResult.Result = BeamSessionStatus.Rejected;
                            beamResult.Status = BeamResultType.Success;
                            break;
                        case BeamOperationStatus.Error:
                            beamResult.Result = BeamSessionStatus.Error;
                            beamResult.Status = BeamResultType.Error;
                            beamResult.Error = operationResult.Error;
                            break;
                        case BeamOperationStatus.NotFound:
                            beamResult.Status = BeamResultType.Error;
                            beamResult.Error = "Operation was not found";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    callback.Invoke(beamResult);
                },
                () =>
                {
                    callback.Invoke(new BeamResult<BeamSessionStatus>(BeamResultType.Timeout, "Timed out polling for Operation result"));
                }, secondsTimeout));

            // retrieve freshly created session
            yield return StartCoroutine(GetActiveSessionFromBeamApi(entityId, session =>
            {
                // store session info in 'cache' for future reference
                m_Storage.Set(BeamSessionStorageKey, JsonConvert.SerializeObject(session));
            }));
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a transaction, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="operationId">Id of the Operation to sign. Returned by Beam API.</param>
        /// <param name="callback">Callback to return a result of signin g</param>
        /// <param name="fallBackToBrowser">If true, opens the browser for the User to perform signing. Defaults to true.</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 30</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator SignOperation(
            string entityId,
            string operationId,
            Action<BeamResult<BeamOperationStatus>> callback,
            bool fallBackToBrowser = true,
            int secondsTimeout = 120)
        {
            EnsureApiKeyIsSet();

            KeyPair activeSessionKeyPair = null;
            yield return StartCoroutine(GetActiveSessionKeyPair(entityId, result =>
            {
                activeSessionKeyPair = result;
            }));

            var hasActiveSession = activeSessionKeyPair != null;
            if (hasActiveSession)
            {
                BeamOperation operation = null;
                yield return StartCoroutine(GetOperationById(operationId, (result) => { operation = result; }));

                if (operation == null)
                {
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Result = BeamOperationStatus.Error,
                        Status = BeamResultType.Error,
                        Error = "Operation was not found"
                    });

                    yield break;
                }

                if (!operation.Transactions.Any())
                {
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Result = BeamOperationStatus.Error,
                        Status = BeamResultType.Error,
                        Error = "Operation has no transactions to sign"
                    });

                    yield break;
                }

                foreach (var transaction in operation.Transactions)
                {
                    var signature = "";
                    switch (transaction.Type)
                    {
                        case BeamOperationTransactionType.OpenfortTransaction:
                            signature = activeSessionKeyPair.Sign(transaction.Data as string);
                            break;
                        case BeamOperationTransactionType.MarketplaceOrder:
                            signature = activeSessionKeyPair.SignTypedData(transaction.Data as TypedData<Beam.Marketplace.Domain>); // todo: check if this actually works
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    transaction.Signature = signature;
                }
                // todo: execute in Beam API
            }
            else if (fallBackToBrowser)
            {
                // open identity.onbeam.com, give it operation id
                Application.OpenURL(
                    $"{m_BeamAuthUrl}/games/{m_BeamGameId}/operation/{operationId}/confirm");

                // start polling for results of the operation
                yield return StartCoroutine(PollForOperationResult(operationId, operationResult =>
                    {
                        var beamResult = new BeamResult<BeamOperationStatus>
                        {
                            Result = operationResult.Status
                        };

                        switch (operationResult.Status)
                        {
                            case BeamOperationStatus.Pending:
                            case BeamOperationStatus.Approved:
                            case BeamOperationStatus.Rejected:
                                beamResult.Status = BeamResultType.Success;
                                break;
                            case BeamOperationStatus.Error:
                                beamResult.Status = BeamResultType.Error;
                                beamResult.Error = operationResult.Error;
                                break;
                            case BeamOperationStatus.NotFound:
                                beamResult.Status = BeamResultType.Error;
                                beamResult.Error = "Operation was not found";
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        callback.Invoke(beamResult);
                    },
                    () =>
                    {
                        callback.Invoke(
                            new BeamResult<BeamOperationStatus>(BeamResultType.Timeout, "Timed out polling for Operation result"));
                    }, secondsTimeout));
            }
            else
            {
                callback.Invoke(new BeamResult<BeamOperationStatus>
                {
                    Result = BeamOperationStatus.Error,
                    Status = BeamResultType.Error,
                    Error = $"No active session and {nameof(fallBackToBrowser)} set to false"
                });
            }
        }

        private void EnsureApiKeyIsSet()
        {
            if (m_BeamPublishableGameKey == null)
            {
                throw new ArgumentNullException("beamApiKey", "Set Beam Api Key first!");
            }
        }

        private IEnumerator CreateOperation(string entityId, int chainId, Action<BeamOperation> onOpIdCreated, Action<string> onError)
        {
            var bodyJson = JsonConvert.SerializeObject(new
            {
                entityId = entityId,
                chainId = chainId,
            });
            var request = UnityWebRequest.Post($"{m_ApiBaseUrl}/v1/self-custody/operation", bodyJson, "application/json");
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
            }
            else
            {
                onError.Invoke(request.error);
            }
        }

        private IEnumerator PollForOperationResult(string opId, Action<BeamOperation> operationResult, Action timeout,
            int secondsTimeout = 30)
        {
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                BeamOperation beamOperation = null;
                yield return GetOperationById(opId, result => { beamOperation = result; });

                if (beamOperation.Status != BeamOperationStatus.Pending)
                {
                    operationResult.Invoke(beamOperation);

                    yield break;
                }

                yield return new WaitForSecondsRealtime(1);
            }

            timeout.Invoke();
        }

        private IEnumerator GetOperationById(string opId, Action<BeamOperation> result)
        {
            var path = $"{m_ApiBaseUrl}/v1/self-custody/operation/{opId}";
            var request = UnityWebRequest.Get(path);
            request.SetRequestHeader(BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 200)
                {
                    var model = JsonConvert.DeserializeObject<BeamOperation>(request.downloadHandler.text);
                    result.Invoke(model);

                    yield break;
                }

                if (request.responseCode == 404)
                {
                    result.Invoke(new BeamOperation
                    {
                        Status = BeamOperationStatus.NotFound,
                        Error = $"Did not find the Operation: {opId}"
                    });

                    yield break;
                }
            }

            result.Invoke(new BeamOperation
            {
                Status = BeamOperationStatus.Error,
                Error = $"Encountered an error when getting Operation: {opId}, error: {request.error}"
            });
        }

        private IEnumerator GetActiveSessionFromBeamApi(string entityId, Action<BeamSessionResult> callback)
        {
            var path = $"{m_ApiBaseUrl}/v1/self-custody/sessions/entities/{entityId}/active";
            var request = UnityWebRequest.Get(path);
            request.SetRequestHeader(BeamAPIKeyHeader, m_BeamPublishableGameKey);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 200)
                {
                    var model = JsonConvert.DeserializeObject<BeamSession>(request.downloadHandler.text);
                    
                    m_Storage.Set(BeamSessionStorageKey, JsonConvert.SerializeObject(model));
                    
                    callback.Invoke(new BeamSessionResult
                    {
                        Success = true,
                        Session = model
                    });

                    yield break;
                }

                if (request.responseCode == 404)
                {
                    callback.Invoke(new BeamSessionResult
                    {
                        Success = false,
                        Error = "Did not find an active Session"
                    });

                    yield break;
                }
            }

            callback.Invoke(new BeamSessionResult
            {
                Success = false,
                Error =
                    $"Received an error when getting active Session. Code: {request.responseCode} Error: {request.error}"
            });
        }

        private IEnumerator GetActiveSessionKeyPair(string entityId, Action<KeyPair> result)
        {
            BeamSession sessionInfoModel = null;
            var sessionInfo = m_Storage.Get(BeamSessionStorageKey);
            if (sessionInfo != null)
            {
                sessionInfoModel = JsonConvert.DeserializeObject<BeamSession>(sessionInfo);
            }

            var now = DateTimeOffset.Now;

            if (sessionInfoModel == null || sessionInfoModel?.StartTime > now || sessionInfoModel?.EndTime < now)
            {
                yield return GetActiveSessionFromBeamApi(entityId, (beamResult) =>
                {
                    if (beamResult.Success && beamResult.Session != null)
                    {
                        sessionInfoModel = beamResult.Session;
                    }
                });
            }

            // todo: think of a way to verify if session in OF was signed with current KeyPair
            if (sessionInfoModel != null && sessionInfoModel?.StartTime <= now && sessionInfoModel?.EndTime > now)
            {
                result.Invoke(GetOrCreateSigningKeyPair());
                yield break;
            }

            m_Storage.Delete(BeamSessionStorageKey);
        }

        private KeyPair GetOrCreateSigningKeyPair()
        {
            var privateKey = m_Storage.Get(BeamSigningKeyStorageKey);
            if (privateKey != null)
            {
                return KeyPair.Load(privateKey);
            }

            var newKeyPair = KeyPair.Generate();
            m_Storage.Set(BeamSigningKeyStorageKey, newKeyPair.PrivateHex);

            return newKeyPair;
        }
    }
}