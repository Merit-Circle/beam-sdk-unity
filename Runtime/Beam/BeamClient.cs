using System;
using System.Collections;
using System.Linq;
using Beam.Api;
using Beam.Models;
using Beam.Util;
using Newtonsoft.Json;
using UnityEngine;

namespace Beam
{
    public class BeamClient : MonoBehaviour
    {
        private const int DEFAULT_TIMEOUT_IN_SECONDS = 240;
        private string m_BeamAuthUrl = string.Empty;
        private string m_BeamGameId = string.Empty;
        private bool m_DebugLog = false;
        private readonly BeamApi m_BeamApi = new();

        private IStorage m_Storage = new PlayerPrefsStorage();

        public BeamClient()
        {
            SetEnvironment(BeamEnvironment.Testnet);
        }

        /// <summary>
        /// Sets Publishable Beam API key on the client. WARNING: Do not use keys other than Publishable, they're meant to be private, server-side only!
        /// </summary>
        /// <param name="gameId">Id of your Beam Game registration</param>
        /// <param name="publishableApiKey">Publishable Beam API key</param>
        /// <returns>BeamClient</returns>
        public BeamClient SetBeamApiGame(string gameId, string publishableApiKey)
        {
            m_BeamApi.SetBeamGameId(gameId);
            m_BeamApi.SetApiKey(publishableApiKey);
            m_BeamGameId = gameId;
            return this;
        }

        /// <summary>
        /// Sets Environment on the client.
        /// </summary>
        /// <param name="environment">BeamEnvironment.Mainnet or BeamEnvironment.Testnet (defaults to Testnet)</param>
        /// <returns>BeamClient</returns>
        public BeamClient SetEnvironment(BeamEnvironment environment)
        {
            string apiUrl;
            switch (environment)
            {
                case BeamEnvironment.Mainnet:
                    m_BeamAuthUrl = "https://identity.onbeam.com";
                    apiUrl = "https://api.onbeam.com";
                    break;
                default:
                case BeamEnvironment.Testnet:
                default:
                    m_BeamAuthUrl = "https://identity.testnet.onbeam.com/";
                    apiUrl = "https://api.testnet.onbeam.com";
                    break;
            }

            m_BeamApi.SetBaseUrl(apiUrl);

            return this;
        }

        /// <summary>
        /// Sets custom storage for Session related information. Defaults to <see cref="PlayerPrefsStorage"/>.
        /// </summary>
        /// <param name="storage">Storage that implements IStorage</param>
        /// <returns>BeamClient</returns>
        public BeamClient SetStorage(IStorage storage)
        {
            m_Storage = storage;
            return this;
        }

        /// <summary>
        /// Set to true, to enable Debug.Log() statements. Defaults to false.
        /// </summary>
        /// <param name="enable">True to enable</param>
        /// <returns>BeamClient</returns>
        public BeamClient SetDebugLogging(bool enable)
        {
            m_DebugLog = enable;
            return this;
        }

        /// <summary>
        /// Retrieves active, valid session.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="actionResult">Callback to return a result with <see cref="BeamSession"/></param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator GetActiveSession(
            string entityId,
            Action<BeamResult<BeamSession>> actionResult,
            int chainId = Constants.DefaultChainId)
        {
            Log("Retrieving active session");

            BeamSession activeSession = null;
            KeyPair keyPair = null;
            yield return StartCoroutine(GetActiveSessionAndKeys(entityId, chainId, (res, kp) =>
            {
                if (res != null)
                {
                    Log(
                        $"Retrieved a session: {res.SessionAddress}, valid from: {res.StartTime:o}, to: {res.EndTime:o}");
                    activeSession = res;
                }

                keyPair = kp;
            }));

            if (activeSession != null)
            {
                Log("No active session found");
                actionResult.Invoke(new BeamResult<BeamSession>(BeamResultType.Error, "No active session found")
                {
                    Result = activeSession
                });
                yield break;
            }

            actionResult.Invoke(new BeamResult<BeamSession>(activeSession));
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a Session, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="actionResult">Callback to return a result of Session creation</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 240</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator CreateSession(
            string entityId,
            Action<BeamResult<BeamSession>> actionResult,
            int chainId = Constants.DefaultChainId,
            int secondsTimeout = DEFAULT_TIMEOUT_IN_SECONDS)
        {
            BeamSession activeSession = null;
            KeyPair keyPair = null;
            Log("Retrieving active session");
            yield return StartCoroutine(GetActiveSessionAndKeys(entityId, chainId, (res, kp) =>
            {
                if (res != null)
                {
                    Log(
                        $"Retrieved a session: {res.SessionAddress}, valid from: {res.StartTime:o}, to: {res.EndTime:o}");
                    activeSession = res;
                }

                keyPair = kp;
            }));

            if (activeSession != null)
            {
                Log("Already has an active session, ending early");
                actionResult.Invoke(new BeamResult<BeamSession>(BeamResultType.Error, "Already has an active session")
                {
                    Result = activeSession
                });
                yield break;
            }

            Log("No active session found, refreshing local KeyPair");

            // refresh keypair to make sure we have no conflicts with existing sessions
            keyPair = GetOrCreateSigningKeyPair(refresh: true);

            // retrieve operation Id to pass further and track result
            BeamSessionRequest beamSessionRequest = null;
            yield return StartCoroutine(m_BeamApi.CreateSessionRequest(entityId, chainId, keyPair.Account.Address,
                res =>
                {
                    if (res.Success)
                    {
                        Log($"Created session request: {res.Result.Id} to check for session result");
                        beamSessionRequest = res.Result;
                    }
                    else
                    {
                        Log($"Failed creating session request: {res.ErrorMessage}");
                        actionResult.Invoke(new BeamResult<BeamSession>(
                            status: BeamResultType.Error,
                            error: res.ErrorMessage));
                    }
                }));

            if (beamSessionRequest == null)
            {
                yield break;
            }

            Log($"Opening {beamSessionRequest.Url}");
            // open identity.onbeam.com
            Application.OpenURL(beamSessionRequest.Url);

            var beamResultModel = new BeamResult<BeamSession>();

            Log("Started polling for Session creation result");
            // start polling for results of the operation
            var error = false;
            yield return StartCoroutine(PollForSessionRequestResult(beamSessionRequest.Id, operationResult =>
                {
                    switch (operationResult.Status)
                    {
                        case BeamSessionRequestStatus.Pending:
                            beamResultModel.Status = BeamResultType.Pending;
                            break;
                        case BeamSessionRequestStatus.Accepted:
                            beamResultModel.Status = BeamResultType.Success;
                            break;
                        case BeamSessionRequestStatus.Error:
                            beamResultModel.Status = BeamResultType.Error;
                            beamResultModel.Error = "Encountered an error when requesting a session";
                            error = true;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                },
                () =>
                {
                    Log("Timed out when polling for Session");
                    error = true;
                    actionResult.Invoke(new BeamResult<BeamSession>(BeamResultType.Timeout,
                        "Timed out polling for Session Request result"));
                }, () =>
                {
                    Log("Session request was not found, something went wrong");
                    error = true;
                    actionResult.Invoke(
                        new BeamResult<BeamSession>(BeamResultType.Error,
                            $"Session Request with id: {beamSessionRequest.Id} could not be found"));
                }, secondsTimeout));

            Log("Retrieving newly created Session");
            // retrieve newly created session
            if (!error)
            {
                yield return StartCoroutine(GetActiveSessionAndKeys(entityId, chainId, (bs, kp) =>
                {
                    if (bs != null)
                    {
                        beamResultModel.Result = bs;
                        Log(
                            $"Retrieved a session: {bs.SessionAddress}, valid from: {bs.StartTime:o}, to: {bs.EndTime:o}");
                    }
                    else
                    {
                        beamResultModel.Error = "Could not retrieve session after it was created";
                        beamResultModel.Status = BeamResultType.Error;
                    }

                    keyPair = kp;
                    actionResult.Invoke(beamResultModel);
                }));
            }
        }

        /// <summary>
        /// A Coroutine that opens an external browser to sign a transaction, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="operationId">Id of the Operation to sign. Returned by Beam API.</param>
        /// <param name="actionResult">Callback to return a result of signin g</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="fallbackToBrowser">If true, opens the browser for the User to create new Session. Defaults to true. Returns an Error if false and there is no active session</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 240</param>
        /// <returns>IEnumerator</returns>
        public IEnumerator SignOperation(
            string entityId,
            string operationId,
            Action<BeamResult<BeamOperationStatus>> actionResult,
            int chainId = Constants.DefaultChainId,
            bool fallbackToBrowser = true,
            int secondsTimeout = DEFAULT_TIMEOUT_IN_SECONDS)
        {
            BeamSession activeSession = null;
            KeyPair activeSessionKeyPair = null;
            Log("Retrieving active session");
            yield return StartCoroutine(GetActiveSessionAndKeys(entityId, chainId, (sessionInfo, keyPair) =>
            {
                activeSession = sessionInfo;
                activeSessionKeyPair = keyPair;
            }));

            var hasActiveSession = activeSessionKeyPair != null && activeSession != null;
            if (hasActiveSession)
            {
                Log($"Has an active session until: {activeSession.EndTime:o}, using it to sign the operation");
                yield return SignOperationUsingSession(
                    entityId,
                    operationId,
                    actionResult,
                    activeSessionKeyPair);
            }
            else if (fallbackToBrowser)
            {
                Log("No active session found, using browser to sign the operation");
                yield return SignOperationUsingBrowser(operationId, actionResult, secondsTimeout);
            }
            else
            {
                Log($"No active session found, {nameof(fallbackToBrowser)} set to false");
                actionResult.Invoke(new BeamResult<BeamOperationStatus>
                {
                    Result = BeamOperationStatus.Error,
                    Status = BeamResultType.Error,
                    Error = $"No active session and {nameof(fallbackToBrowser)} set to false"
                });
            }
        }

        private object SignOperationUsingBrowser(
            string operationId,
            Action<BeamResult<BeamOperationStatus>> callback,
            int secondsTimeout)
        {
            var url = $"{m_BeamAuthUrl}/games/{m_BeamGameId}/operation/{operationId}/confirm";
            Log($"Opening {url}");

            // open identity.onbeam.com, give it operation id
            Application.OpenURL(url);

            // start polling for results of the operation
            return StartCoroutine(PollForOperationResult(operationId, operationResult =>
                {
                    Log($"Got operation({operationId}) result: {operationResult.Status.ToString()}");
                    var beamResult = new BeamResult<BeamOperationStatus>
                    {
                        Result = operationResult.Status
                    };

                    switch (operationResult.Status)
                    {
                        case BeamOperationStatus.Pending:
                        case BeamOperationStatus.Executed:
                        case BeamOperationStatus.Rejected:
                            beamResult.Status = BeamResultType.Success;
                            break;
                        case BeamOperationStatus.Error:
                            beamResult.Status = BeamResultType.Error;
                            beamResult.Error = operationResult.Error;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    callback.Invoke(beamResult);
                },
                () =>
                {
                    Log($"Timed out polling for Operation({operationId}) result");
                    callback.Invoke(
                        new BeamResult<BeamOperationStatus>(BeamResultType.Timeout,
                            "Timed out polling for Operation result"));
                }, () =>
                {
                    Log($"Operation with id: {operationId} could not be found, something went wrong");
                    callback.Invoke(
                        new BeamResult<BeamOperationStatus>(BeamResultType.Error,
                            $"Operation with id: {operationId} could not be found"));
                }, secondsTimeout));
        }

        private IEnumerator SignOperationUsingSession(
            string entityId,
            string operationId,
            Action<BeamResult<BeamOperationStatus>> callback,
            KeyPair activeSessionKeyPair)
        {
            BeamOperation operation = null;
            Log($"Retrieving operation({operationId})");
            yield return StartCoroutine(m_BeamApi.GetOperationById(operationId, (res) =>
            {
                if (res.Success)
                {
                    operation = res.Result;
                }
                else if (res.StatusCode != 404) // 404 will be handled as operation == null below
                {
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Status = BeamResultType.Error,
                        Error = res.ErrorMessage
                    });
                }
            }));

            if (operation == null)
            {
                Log($"No operation({operationId}) was found, ending");
                callback.Invoke(new BeamResult<BeamOperationStatus>
                {
                    Result = BeamOperationStatus.Error,
                    Status = BeamResultType.Error,
                    Error = "Operation was not found"
                });

                yield break;
            }

            if (operation?.Transactions?.Any() != true)
            {
                Log($"Operation({operationId}) has no transactions to sign, ending");
                callback.Invoke(new BeamResult<BeamOperationStatus>
                {
                    Result = BeamOperationStatus.Error,
                    Status = BeamResultType.Error,
                    Error = "Operation has no transactions to sign"
                });

                yield break;
            }

            var confirmationModel = new BeamOperationConfirmation
            {
                GameId = m_BeamGameId,
                EntityId = entityId,
                Status = BeamOperationStatus.Pending
            };

            foreach (var transaction in operation.Transactions)
            {
                Log($"Signing operation({operationId}) transaction({transaction.ExternalId})");
                string signature;
                try
                {
                    switch (transaction.Type)
                    {
                        case BeamOperationTransactionType.OpenfortTransaction:
                            signature = activeSessionKeyPair.SignMessage(transaction.Data);
                            break;
                        case BeamOperationTransactionType.OpenfortReservoirOrder:
                            signature = activeSessionKeyPair.SignMarketplaceTransactionHash(transaction.Hash);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    confirmationModel.Transactions.Add(new TransactionConfirmation
                    {
                        Id = transaction.Id,
                        Signature = signature
                    });
                }
                catch (Exception e)
                {
                    Log($"Encountered an error when signing transaction({transaction.Id}): {e.Message}");
                    confirmationModel.Status = BeamOperationStatus.Error;
                    confirmationModel.Error =
                        $"Encountered an exception while approving {transaction.Type.ToString()}: {e.Message}";
                }
            }

            Log($"Confirming operation({operationId})");
            yield return StartCoroutine(m_BeamApi.ConfirmOperation(operationId, confirmationModel, res =>
            {
                if (res.Success)
                {
                    var didFail = res.Result.Status != BeamOperationStatus.Executed &&
                                  res.Result.Status != BeamOperationStatus.Pending;

                    Log(
                        $"Confirming operation({operationId}) status: {res.Result.Status.ToString()} error: {res.Result.Error}");
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Status = didFail ? BeamResultType.Error : BeamResultType.Success,
                        Result = res.Result.Status,
                        Error = didFail ? res.ErrorMessage : null
                    });
                }
                else
                {
                    Log($"Confirming operation({operationId}) encountered an error: {res.ErrorMessage}");
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Result = BeamOperationStatus.Error,
                        Status = BeamResultType.Error,
                        Error = res.ErrorMessage ?? $"Encountered unknown error when confirming operation {operationId}"
                    });
                }
            }));
        }

        private IEnumerator PollForOperationResult(
            string opId,
            Action<BeamOperation> operationResult,
            Action timeout,
            Action operationNotFound,
            int secondsTimeout = DEFAULT_TIMEOUT_IN_SECONDS,
            int secondsBetweenPolls = 1)
        {
            var now = DateTimeOffset.Now;
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                BeamOperation beamOperation = null;
                var opNotFound = false;
                yield return m_BeamApi.GetOperationById(opId, res =>
                {
                    if (res.Success)
                    {
                        beamOperation = res.Result;
                    }
                    else if (res.StatusCode == 404)
                    {
                        opNotFound = true;
                    }
                });

                if (opNotFound)
                {
                    operationNotFound.Invoke();
                    yield break;
                }

                // check for status as well as updatedAt
                // in case of retries, status can be set to Error, but we're interested in the newest actual result
                if (beamOperation.Status != BeamOperationStatus.Pending &&
                    (beamOperation.UpdatedAt != null && beamOperation.UpdatedAt > now))
                {
                    operationResult.Invoke(beamOperation);

                    yield break;
                }

                yield return new WaitForSecondsRealtime(secondsBetweenPolls);
            }

            timeout.Invoke();
        }

        private IEnumerator PollForSessionRequestResult(
            string sessionRequestId,
            Action<BeamSessionRequest> sessionRequestResult,
            Action timeout,
            Action operationNotFound,
            int secondsTimeout = DEFAULT_TIMEOUT_IN_SECONDS,
            int secondsBetweenPolls = 1)
        {
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                BeamSessionRequest beamSessionRequest = null;
                var sessionRequestNotFound = false;
                yield return m_BeamApi.GetSessionRequestById(sessionRequestId, res =>
                {
                    if (res.Success)
                    {
                        beamSessionRequest = res.Result;
                    }
                    else if (res.StatusCode == 404)
                    {
                        sessionRequestNotFound = true;
                    }
                });

                if (sessionRequestNotFound)
                {
                    operationNotFound.Invoke();
                    yield break;
                }

                // check for status as well as updatedAt
                // in case of retries, status can be set to Error, but we're interested in the newest actual result
                if (beamSessionRequest.Status != BeamSessionRequestStatus.Pending)
                {
                    sessionRequestResult.Invoke(beamSessionRequest);

                    yield break;
                }

                yield return new WaitForSecondsRealtime(secondsBetweenPolls);
            }

            timeout.Invoke();
        }

        private IEnumerator GetActiveSessionAndKeys(
            string entityId,
            int chainId,
            Action<BeamSession, KeyPair> activeSession)
        {
            BeamSession beamSession = null;
            var sessionInfo = m_Storage.Get(Constants.Storage.BeamSession);
            if (sessionInfo != null)
            {
                beamSession = JsonConvert.DeserializeObject<BeamSession>(sessionInfo);
            }

            var keyPair = GetOrCreateSigningKeyPair();

            // if session is no longer valid, check if we have one saved in the API
            if (!beamSession.IsValidNow())
            {
                yield return m_BeamApi.GetActiveSessionInfo(entityId, keyPair.Account.Address, (res) =>
                {
                    if (res.Success && res.Result != null)
                    {
                        beamSession = res.Result;
                    }
                }, chainId);
            }

            // make sure session we just retrieved is valid and owned by current KeyPair
            if (beamSession.IsValidNow() && beamSession.IsOwnedBy(keyPair))
            {
                m_Storage.Set(Constants.Storage.BeamSession, JsonConvert.SerializeObject(beamSession));
                activeSession.Invoke(beamSession, keyPair);
                yield break;
            }

            // if session is not valid or owned by different KeyPair, remove it from cache
            m_Storage.Delete(Constants.Storage.BeamSession);
            activeSession.Invoke(null, keyPair);
        }

        private KeyPair GetOrCreateSigningKeyPair(bool refresh = false)
        {
            if (!refresh)
            {
                var privateKey = m_Storage.Get(Constants.Storage.BeamSigningKey);
                if (privateKey != null)
                {
                    return KeyPair.Load(privateKey);
                }
            }

            var newKeyPair = KeyPair.Generate();
            m_Storage.Set(Constants.Storage.BeamSigningKey, newKeyPair.PrivateHex);

            return newKeyPair;
        }

        private void Log(string message)
        {
            if (m_DebugLog)
            {
                Debug.Log(message);
            }
        }
    }
}