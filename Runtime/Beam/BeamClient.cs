using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Beam.Api;
using Beam.Extensions;
using Beam.Models;
using Beam.Storage;
using Beam.Util;
using BeamPlayerClient.Api;
using BeamPlayerClient.Client;
using BeamPlayerClient.Model;
using Newtonsoft.Json;
using UnityEngine;

namespace Beam
{
    public class BeamClient : MonoBehaviour
    {
        public IAssetsApi AssetsApi => new AssetsApi(GetConfiguration());
        public IExchangeApi ExchangeApi => new ExchangeApi(GetConfiguration());
        public IHealthApi HealthApi => new HealthApi(GetConfiguration());
        public IMarketplaceApi MarketplaceApi => new MarketplaceApi(GetConfiguration());
        public ISessionsApi SessionsApi => new SessionsApi(GetConfiguration());
        public ITransactionsApi TransactionsApi => new TransactionsApi(GetConfiguration());
        public IUsersApi UsersApi => new UsersApi(GetConfiguration());
        public IOperationApi OperationApi => new OperationApi(GetConfiguration());

        private const int DefaultTimeoutInSeconds = 240;

        private readonly BeamCoroutineApi m_BeamCoroutineApi = new();

        private string m_BeamApiKey = null;
        private string m_BeamApiUrl = null;
        private bool m_DebugLog = false;
        private IStorage m_Storage = new PlayerPrefsStorage();

        public BeamClient()
        {
            SetEnvironment(BeamEnvironment.Testnet);
        }

        /// <summary>
        /// Sets Publishable Beam API key on the client. WARNING: Do not use keys other than Publishable, they're meant to be private, server-side only!
        /// </summary>
        /// <param name="publishableApiKey">Publishable Beam API key</param>
        /// <returns>BeamClient</returns>
        public BeamClient SetBeamApiKey(string publishableApiKey)
        {
            m_BeamApiKey = publishableApiKey;
            m_BeamCoroutineApi.SetApiKey(publishableApiKey);
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
                    apiUrl = "https://api.onbeam.com";
                    break;
                default:
                    apiUrl = "https://api.preview.onbeam.com"; // todo: bring back to testnet
                    break;
            }

            m_BeamApiUrl = apiUrl;
            m_BeamCoroutineApi.SetBaseUrl(apiUrl);

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

            if (activeSession == null)
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
            int secondsTimeout = DefaultTimeoutInSeconds)
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
            yield return StartCoroutine(m_BeamCoroutineApi.CreateSessionRequest(entityId, chainId,
                keyPair.Account.Address,
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
            int secondsTimeout = DefaultTimeoutInSeconds)
        {
            BeamSession activeSession = null;
            KeyPair activeSessionKeyPair = null;
            Log("Retrieving active session");
            yield return StartCoroutine(GetActiveSessionAndKeys(entityId, chainId, (sessionInfo, keyPair) =>
            {
                activeSession = sessionInfo;
                activeSessionKeyPair = keyPair;
            }));

            BeamOperation operation = null;
            Log($"Retrieving operation({operationId})");
            yield return StartCoroutine(m_BeamCoroutineApi.GetOperationById(operationId, (res) =>
            {
                if (res.Success)
                {
                    operation = res.Result;
                }
                else if (res.StatusCode != 404) // 404 will be handled as operation == null below
                {
                    actionResult.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Status = BeamResultType.Error,
                        Error = res.ErrorMessage
                    });
                }
            }));

            if (operation == null)
            {
                Log($"No operation({operationId}) was found, ending");
                actionResult.Invoke(new BeamResult<BeamOperationStatus>
                {
                    Status = BeamResultType.Error,
                    Error = "Operation was not found"
                });

                yield break;
            }

            var hasActiveSession = activeSessionKeyPair != null && activeSession != null;
            if (hasActiveSession)
            {
                Log($"Has an active session until: {activeSession.EndTime:o}, using it to sign the operation");
                yield return SignOperationUsingSession(
                    entityId,
                    operation,
                    actionResult,
                    activeSessionKeyPair);
            }
            else if (fallbackToBrowser)
            {
                Log("No active session found, using browser to sign the operation");
                yield return SignOperationUsingBrowser(operation, actionResult, secondsTimeout);
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

        private IEnumerator SignOperationUsingBrowser(
            BeamOperation operation,
            Action<BeamResult<BeamOperationStatus>> callback,
            int secondsTimeout)
        {
            var url = operation.Url;
            Log($"Opening {url}");

            // open identity.onbeam.com, give it operation id
            Application.OpenURL(url);

            // start polling for results of the operation
            yield return StartCoroutine(PollForOperationResult(operation.Id, operationResult =>
                {
                    Log($"Got operation({operation.Id}) result: {operationResult.Status.ToString()}");
                    var beamResult = new BeamResult<BeamOperationStatus>
                    {
                        Result = operationResult.Status
                    };

                    switch (operationResult.Status)
                    {
                        case BeamOperationStatus.Pending:
                        case BeamOperationStatus.Executed:
                        case BeamOperationStatus.Rejected:
                        case BeamOperationStatus.Signed:
                            beamResult.Status = BeamResultType.Success;
                            break;
                        case BeamOperationStatus.Error:
                            beamResult.Status = BeamResultType.Error;
                            beamResult.Error = "Operation encountered an error";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    callback.Invoke(beamResult);
                },
                () =>
                {
                    Log($"Timed out polling for Operation({operation.Id}) result");
                    callback.Invoke(
                        new BeamResult<BeamOperationStatus>(BeamResultType.Timeout,
                            "Timed out polling for Operation result"));
                }, () =>
                {
                    Log($"Operation with id: {operation.Id} could not be found, something went wrong");
                    callback.Invoke(
                        new BeamResult<BeamOperationStatus>(BeamResultType.Error,
                            $"Operation with id: {operation.Id} could not be found"));
                }, secondsTimeout));
        }
        
        private async Task<BeamResult<CommonOperationResponse.StatusEnum>> SignOperationUsingBrowserAsync(
            BeamOperation operation,
            int secondsTimeout,
            CancellationToken cancellationToken = default)
        {
            var url = operation.Url;
            Log($"Opening {url}");

            // open identity.onbeam.com, give it operation id
            Application.OpenURL(url);

            // start polling for results of the operation
            var res = await PollForOperationResultAsync(operation.Id, secondsTimeout,
                cancellationToken: cancellationToken);

            Log($"Got operation({operation.Id}) result: {res.Status.ToString()}");
            var beamResult = new BeamResult<CommonOperationResponse.StatusEnum>
            {
                Result = res.Status
            };

            switch (res.Status)
            {
                case CommonOperationResponse.StatusEnum.Pending:
                case CommonOperationResponse.StatusEnum.Executed:
                case CommonOperationResponse.StatusEnum.Rejected:
                case CommonOperationResponse.StatusEnum.Signed:
                    beamResult.Status = BeamResultType.Success;
                    break;
                case CommonOperationResponse.StatusEnum.Error:
                    beamResult.Status = BeamResultType.Error;
                    beamResult.Error = "Operation encountered an error";
                    break;
                default:
                    beamResult.Status = BeamResultType.Error;
                    beamResult.Error = "Polling for operation encountered an error or timed out";
                    break;
            }

            return beamResult;
        }

        private IEnumerator SignOperationUsingSession(
            string entityId,
            BeamOperation operation,
            Action<BeamResult<BeamOperationStatus>> callback,
            KeyPair activeSessionKeyPair)
        {
            if (operation?.Transactions?.Any() != true)
            {
                Log($"Operation({operation.Id}) has no transactions to sign, ending");
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
                GameId = operation.GameId,
                EntityId = entityId,
                Status = BeamOperationStatus.Pending
            };

            foreach (var transaction in operation.Transactions)
            {
                Log($"Signing operation({operation.Id}) transaction({transaction.ExternalId})");
                string signature;
                try
                {
                    switch (transaction.Type)
                    {
                        case BeamOperationTransactionType.OpenfortTransaction:
                        case BeamOperationTransactionType.OpenfortRevokeSession:
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

            Log($"Confirming operation({operation.Id})");
            yield return StartCoroutine(m_BeamCoroutineApi.ConfirmOperation(operation.Id, confirmationModel, res =>
            {
                if (res.Success)
                {
                    var didFail = res.Result.Status != BeamOperationStatus.Executed &&
                                  res.Result.Status != BeamOperationStatus.Signed &&
                                  res.Result.Status != BeamOperationStatus.Pending;

                    Log(
                        $"Confirming operation({operation.Id}) status: {res.Result.Status.ToString()} error: {res.Error}");
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Status = didFail ? BeamResultType.Error : BeamResultType.Success,
                        Result = res.Result.Status,
                        Error = didFail ? res.ErrorMessage : null
                    });
                }
                else
                {
                    Log($"Confirming operation({operation.Id}) encountered an error: {res.ErrorMessage}");
                    callback.Invoke(new BeamResult<BeamOperationStatus>
                    {
                        Result = BeamOperationStatus.Error,
                        Status = BeamResultType.Error,
                        Error = res.ErrorMessage ??
                                $"Encountered unknown error when confirming operation {operation.Id}"
                    });
                }
            }));
        }

        private async Task<BeamResult<CommonOperationResponse.StatusEnum>> SignOperationUsingSessionAsync(
            string entityId,
            BeamOperation operation,
            KeyPair activeSessionKeyPair,
            CancellationToken cancellationToken = default)
        {
            if (operation?.Transactions?.Any() != true)
            {
                Log($"Operation({operation.Id}) has no transactions to sign, ending");
                return new BeamResult<CommonOperationResponse.StatusEnum>
                {
                    Result = CommonOperationResponse.StatusEnum.Error,
                    Status = BeamResultType.Error,
                    Error = "Operation has no transactions to sign"
                };
            }

            var confirmationModel = new ConfirmOperationRequest
            {
                GameId = operation.GameId,
                EntityId = entityId,
                Status = ConfirmOperationRequest.StatusEnum.Pending
            };

            foreach (var transaction in operation.Transactions)
            {
                Log($"Signing operation({operation.Id}) transaction({transaction.ExternalId})");
                string signature;
                try
                {
                    switch (transaction.Type)
                    {
                        case BeamOperationTransactionType.OpenfortTransaction:
                        case BeamOperationTransactionType.OpenfortRevokeSession:
                            signature = activeSessionKeyPair.SignMessage(transaction.Data);
                            break;
                        case BeamOperationTransactionType.OpenfortReservoirOrder:
                            signature = activeSessionKeyPair.SignMarketplaceTransactionHash(transaction.Hash);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    confirmationModel.Transactions.Add(new ConfirmOperationRequestTransactionsInner
                    {
                        Id = transaction.Id,
                        Signature = signature
                    });
                }
                catch (Exception e)
                {
                    Log($"Encountered an error when signing transaction({transaction.Id}): {e.Message}");
                    return new BeamResult<CommonOperationResponse.StatusEnum>
                    {
                        Status = BeamResultType.Error,
                        Result = CommonOperationResponse.StatusEnum.Error,
                        Error = $"Encountered an exception while approving {transaction.Type.ToString()}: {e.Message}"
                    };
                }
            }

            Log($"Confirming operation({operation.Id})");
            var res = await OperationApi.ProcessOperationWithHttpInfoAsync(operation.Id, confirmationModel,
                cancellationToken);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var didFail = res.Data.Status != CommonOperationResponse.StatusEnum.Executed &&
                              res.Data.Status != CommonOperationResponse.StatusEnum.Signed &&
                              res.Data.Status != CommonOperationResponse.StatusEnum.Pending;

                Log(
                    $"Confirming operation({operation.Id}) status: {res.Data.Status.ToString()} error: {res.RawContent}");
                return new BeamResult<CommonOperationResponse.StatusEnum>
                {
                    Status = didFail ? BeamResultType.Error : BeamResultType.Success,
                    Result = res.Data?.Status ?? CommonOperationResponse.StatusEnum.Error,
                    Error = didFail ? res.RawContent : null
                };
            }

            Log($"Confirming operation({operation.Id}) encountered an error: {res.RawContent}");
            return new BeamResult<CommonOperationResponse.StatusEnum>
            {
                Result = CommonOperationResponse.StatusEnum.Error,
                Status = BeamResultType.Error,
                Error = res.RawContent ??
                        $"Encountered unknown error when confirming operation {operation.Id}"
            };
        }

        private IEnumerator PollForOperationResult(
            string opId,
            Action<BeamOperation> operationResult,
            Action timeout,
            Action operationNotFound,
            int secondsTimeout = DefaultTimeoutInSeconds,
            int secondsBetweenPolls = 1)
        {
            var now = DateTimeOffset.Now;
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                BeamOperation beamOperation = null;
                var opNotFound = false;
                yield return m_BeamCoroutineApi.GetOperationById(opId, res =>
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

        private async Task<CommonOperationResponse> PollForOperationResultAsync(
            string opId,
            int secondsTimeout = DefaultTimeoutInSeconds,
            int secondsBetweenPolls = 1,
            CancellationToken cancellationToken = default)
        {
            var now = DateTimeOffset.Now;
            await Task.Delay(2000, cancellationToken);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                CommonOperationResponse beamOperation = null;
                var res = await OperationApi.GetOperationWithHttpInfoAsync(opId, cancellationToken);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    beamOperation = res.Data;
                }
                else if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                // check for status as well as updatedAt
                // in case of retries, status can be set to Error, but we're interested in the newest actual result
                if (beamOperation != null &&
                    beamOperation.Status != CommonOperationResponse.StatusEnum.Pending &&
                    beamOperation.UpdatedAt != null && beamOperation.UpdatedAt > now)
                {
                    return beamOperation;
                }

                await Task.Delay(secondsBetweenPolls * 1000, cancellationToken);
            }

            return null;
        }

        private IEnumerator PollForSessionRequestResult(
            string sessionRequestId,
            Action<BeamSessionRequest> sessionRequestResult,
            Action timeout,
            Action operationNotFound,
            int secondsTimeout = DefaultTimeoutInSeconds,
            int secondsBetweenPolls = 1)
        {
            yield return new WaitForSecondsRealtime(2);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                BeamSessionRequest beamSessionRequest = null;
                var sessionRequestNotFound = false;
                yield return m_BeamCoroutineApi.GetSessionRequestById(sessionRequestId, res =>
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

        private async Task<GetSessionRequestResponse> PollForSessionRequestResultAsync(
            string sessionRequestId,
            int secondsTimeout = DefaultTimeoutInSeconds,
            int secondsBetweenPolls = 1,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(2000, cancellationToken);

            var endTime = DateTime.Now.AddSeconds(secondsTimeout);

            while ((endTime - DateTime.Now).TotalSeconds > 0)
            {
                GetSessionRequestResponse beamSessionRequest = null;
                var res = await SessionsApi.GetSessionRequestWithHttpInfoAsync(sessionRequestId, cancellationToken);

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    beamSessionRequest = res.Data;
                }
                else if (res.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                if (beamSessionRequest?.Status != GetSessionRequestResponse.StatusEnum.Pending)
                {
                    return beamSessionRequest;
                }

                await Task.Delay(secondsBetweenPolls * 1000, cancellationToken);
            }

            return null;
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
                yield return m_BeamCoroutineApi.GetActiveSessionInfo(entityId, keyPair.Account.Address, (res) =>
                {
                    if (res.Success && res.Result != null)
                    {
                        beamSession = res.Result;
                    }
                    else
                    {
                        Log($"GetActiveSessionInfo returned: {res.ErrorMessage}");
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

        private async Task<BeamSession> GetActiveSessionAndKeysAsync(
            string entityId,
            int chainId,
            CancellationToken cancellationToken = default)
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
                var res = await SessionsApi.GetActiveSessionWithHttpInfoAsync(entityId, keyPair.Account.Address,
                    chainId, cancellationToken);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    beamSession = new BeamSession
                    {
                        Id = res.Data.Id,
                        StartTime = res.Data.EndTime,
                        EndTime = res.Data.EndTime,
                        SessionAddress = res.Data.SessionAddress
                    };
                }
                else
                {
                    Log($"GetActiveSessionInfo returned: {res.RawContent}");
                }
            }

            // make sure session we just retrieved is valid and owned by current KeyPair
            if (beamSession.IsValidNow() && beamSession.IsOwnedBy(keyPair))
            {
                m_Storage.Set(Constants.Storage.BeamSession, JsonConvert.SerializeObject(beamSession));
                return beamSession;
            }

            // if session is not valid or owned by different KeyPair, remove it from cache
            m_Storage.Delete(Constants.Storage.BeamSession);
            return null;
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

        private Configuration GetConfiguration()
        {
            var config = new Configuration
            {
                BasePath = m_BeamApiUrl
            };
            config.ApiKey.Add("x-api-key", m_BeamApiKey);
            config.DefaultHeaders.Add("x-beam-sdk", "unity");
            return config;
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