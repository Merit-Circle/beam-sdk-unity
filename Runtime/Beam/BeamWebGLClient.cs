using System;
using System.Threading;
using System.Threading.Tasks;
using Beam.Models;
using Beam.Storage;
using BeamPlayerClient.Client;
using BeamPlayerClient.Model;
using Cysharp.Threading.Tasks;

namespace Beam
{
    /// <summary>
    /// Subclass of <see cref="BeamClient"/> ment for WebGL based applications.
    /// Splits all flows into two steps, so that when Application.OpenURL is called, it is not blocked by popup blockers.
    /// NOTE: This is still a subject to change!
    /// </summary>
    public class BeamWebGLClient : BeamClient
    {
        #region Config
        
        /// <summary>
        /// Sets Publishable Beam API key on the client. WARNING: Do not use keys other than Publishable, they're meant to be private, server-side only!
        /// </summary>
        /// <param name="publishableApiKey">Publishable Beam API key</param>
        /// <returns>BeamClient</returns>
        public BeamWebGLClient SetBeamApiKey(string publishableApiKey)
        {
            base.SetBeamApiKey(publishableApiKey);
            return this;
        }

        /// <summary>
        /// Sets Environment on the client.
        /// </summary>
        /// <param name="environment">BeamEnvironment.Mainnet or BeamEnvironment.Testnet (defaults to Testnet)</param>
        /// <returns>BeamClient</returns>
        public BeamWebGLClient SetEnvironment(BeamEnvironment environment)
        {
            base.SetEnvironment(environment);
            return this;
        }

        /// <summary>
        /// Sets custom storage for Session related information. Defaults to <see cref="PlayerPrefsStorage"/>.
        /// </summary>
        /// <param name="storage">Storage that implements IStorage</param>
        /// <returns>BeamClient</returns>
        public BeamWebGLClient SetStorage(IStorage storage)
        {
            base.SetStorage(storage);
            return this;
        }

        /// <summary>
        /// Set to true, to enable Debug.Log() statements. Defaults to false.
        /// </summary>
        /// <param name="enable">True to enable</param>
        /// <returns>BeamClient</returns>
        public BeamWebGLClient SetDebugLogging(bool enable)
        {
            base.SetDebugLogging(enable);
            return this;
        }

        /// <summary>
        /// Sets a custom callback that should open URLs. By default uses Application.OpenUrl().
        /// Might be useful when running WebGL to avoid popup blocking, by using various js interop plugins, or when custom WebView is needed.
        /// </summary>
        /// <param name="url">Url to open in a browser or webview. Must keep all query params and casing to work.</param>
        /// <returns>BeamClient</returns>
        public BeamWebGLClient SetUrlOpener(Action<string> url)
        {
            base.SetUrlOpener(url);
            return this;
        }

        #endregion
        
        /// <summary>
        /// Retrieves Connection Request data. Use with <see cref="StartConnectingUserToGameAsync"/>
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<CreateConnectionRequestResponse>> GetUserConnectionRequestAsync(
            string entityId,
            int chainId = Constants.DefaultChainId,
            CancellationToken cancellationToken = default)
        {
            Log("Retrieving connection request");
            CreateConnectionRequestResponse connRequest;
            try
            {
                connRequest = await ConnectorApi.CreateConnectionRequestAsync(
                    new CreateConnectionRequestInput(entityId, chainId), cancellationToken);

                return new BeamResult<CreateConnectionRequestResponse>(connRequest);
            }
            catch (ApiException e)
            {
                return new BeamResult<CreateConnectionRequestResponse>(BeamResultType.Error, e.Message);
            }
        }

        /// <summary>
        /// Will connect given EntityId for your game to a User.
        /// This will also happen on first possible action signed by user in the browser.
        /// </summary>
        /// <param name="createConnectionRequestResponse">ConnectionRequest from <see cref="GetUserConnectionRequestAsync"/></param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 240</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<GetConnectionRequestResponse.StatusEnum>> StartConnectingUserToGameAsync(
            CreateConnectionRequestResponse createConnectionRequestResponse,
            int secondsTimeout = DefaultTimeoutInSeconds,
            CancellationToken cancellationToken = default)
        {
            Log($"Opening ${createConnectionRequestResponse.Url}");
            // open browser to connect user
            m_UrlToOpen(createConnectionRequestResponse.Url);

            var pollingResult = await PollForResult(
                actionToPerform: () => ConnectorApi.GetConnectionRequestAsync(createConnectionRequestResponse.Id, cancellationToken),
                shouldRetry: res => res.Status == GetConnectionRequestResponse.StatusEnum.Pending,
                secondsTimeout: secondsTimeout,
                secondsBetweenPolls: 1,
                cancellationToken: cancellationToken);

            Log($"Got polling connection request result: {pollingResult.Status.ToString()}");

            return new BeamResult<GetConnectionRequestResponse.StatusEnum>(pollingResult.Status);
        }
        
        /// <summary>
        /// Retrieves RevokeSession Operation data. Use with <see cref="StartSessionRevokingAsync"/>
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="sessionAddress">address of a Session to revoke</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<CommonOperationResponse>> GetSessionRevokingOperationAsync(
            string entityId,
            string sessionAddress,
            int chainId = Constants.DefaultChainId,
            CancellationToken cancellationToken = default)
        {
            Log("Retrieving active session");

            CommonOperationResponse operation;

            try
            {
                operation = await SessionsApi.RevokeSessionAsync(entityId,
                    new RevokeSessionRequestInput(sessionAddress, chainId: chainId), cancellationToken);
            }
            catch (ApiException e)
            {
                Log($"Failed RevokeSessionAsync: {e.Message} {e.ErrorContent}");
                return new BeamResult<CommonOperationResponse>(BeamResultType.Error, e.Message);
            }

            return new BeamResult<CommonOperationResponse>(operation);
        }

        /// <summary>
        /// Revokes given Session Address. Always opens Browser as User has to sign it with his key.
        /// </summary>
        /// <param name="operation">SessionRevoke Operation from <see cref="GetSessionRevokingOperationAsync"/></param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 240</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async Task<BeamResult<CommonOperationResponse.StatusEnum>> StartSessionRevokingAsync(
            CommonOperationResponse operation,
            int chainId = Constants.DefaultChainId,
            int secondsTimeout = DefaultTimeoutInSeconds,
            CancellationToken cancellationToken = default)
        {
            var result = await SignOperationUsingBrowserAsync(operation, secondsTimeout, cancellationToken);
            return result;
        }
        
        /// <summary>
        /// Retrieves payload to start Session Creation signing. Use with <see cref="StartSessionSigningAsync"/>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="suggestedExpiry">Suggested expiration date for Session. It will be presented in the identity.onbea.com as pre-selected.</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        /// </summary>
        public async UniTask<BeamResult<GenerateSessionRequestResponse>> GetSessionSigningRequestAsync(
            string entityId,
            DateTime? suggestedExpiry = null,
            int chainId = Constants.DefaultChainId,
            CancellationToken cancellationToken = default)
        {
            Log("Retrieving active session");
            var (activeSession, _) = await GetActiveSessionAndKeysAsync(entityId, chainId, cancellationToken);

            if (activeSession != null)
            {
                Log("Already has an active session, ending early");
                return new BeamResult<GenerateSessionRequestResponse>(BeamResultType.Error, "Already has an active session");
            }

            Log("No active session found, refreshing local KeyPair");

            // refresh keypair to make sure we have no conflicts with existing sessions
            var newKeyPair = GetOrCreateSigningKeyPair(entityId, refresh: true);

            // retrieve operation Id to pass further and track result
            GenerateSessionRequestResponse beamSessionRequest;
            try
            {
                var res = await SessionsApi.CreateSessionRequestAsync(entityId,
                    new GenerateSessionUrlRequestInput(newKeyPair.Account.Address, suggestedExpiry: suggestedExpiry, chainId: chainId), cancellationToken);

                Log($"Created session request: {res.Id} to check for session result");
                return new BeamResult<GenerateSessionRequestResponse>(res);
            }
            catch (ApiException e)
            {
                Log($"Failed creating session request: {e.Message} {e.ErrorContent}");
                return new BeamResult<GenerateSessionRequestResponse>(e);
            }
        }

        /// <summary>
        /// Opens an external browser to sign a Session, returns the result via callback arg.
        /// </summary>
        /// <param name="generateSessionRequestResponse">SessionRequest from <see cref="GetSessionSigningRequestAsync"/></param>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<BeamSession>> StartSessionSigningAsync(
            GenerateSessionRequestResponse generateSessionRequestResponse,
            string entityId,
            int chainId = Constants.DefaultChainId,
            CancellationToken cancellationToken = default)
        {
            Log($"Opening {generateSessionRequestResponse.Url}");
            // open identity.onbeam.com
            m_UrlToOpen(generateSessionRequestResponse.Url);

            var beamResultModel = new BeamResult<BeamSession>();

            Log("Started polling for Session creation result");
            // start polling for results of the operation
            var error = false;

            var pollingResult = await PollForResult(
                actionToPerform: () => SessionsApi.GetSessionRequestAsync(generateSessionRequestResponse.Id, cancellationToken),
                shouldRetry: res => res.Status == GetSessionRequestResponse.StatusEnum.Pending,
                secondsTimeout: 600,
                secondsBetweenPolls: 1,
                cancellationToken: cancellationToken);

            if (pollingResult == null)
            {
                return new BeamResult<BeamSession>(BeamResultType.Error,
                    "Polling for created session encountered an error or timed out");
            }

            switch (pollingResult.Status)
            {
                case GetSessionRequestResponse.StatusEnum.Pending:
                    beamResultModel.Status = BeamResultType.Pending;
                    break;
                case GetSessionRequestResponse.StatusEnum.Accepted:
                    beamResultModel.Status = BeamResultType.Success;
                    break;
                case GetSessionRequestResponse.StatusEnum.Error:
                    beamResultModel.Status = BeamResultType.Error;
                    beamResultModel.Error = "Encountered an error when requesting a session";
                    error = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Log("Retrieving newly created Session");
            // retrieve newly created session
            if (!error)
            {
                var (beamSession, _) = await GetActiveSessionAndKeysAsync(entityId, chainId, cancellationToken);
                if (beamSession != null)
                {
                    beamResultModel.Result = beamSession;
                    Log(
                        $"Retrieved a session: {beamSession.SessionAddress}, valid from: {beamSession.StartTime:o}, to: {beamSession.EndTime:o}");
                }
                else
                {
                    beamResultModel.Error = "Could not retrieve session after it was created";
                    beamResultModel.Status = BeamResultType.Error;
                }
            }

            return beamResultModel;
        }

        /// <summary>
        /// Retrieves Operation to sign. Use with <see cref="StartOperationSigningAsync"/>
        /// </summary>
        /// <param name="operationId">Id of the Operation to sign. Returned by Beam API.</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<CommonOperationResponse>> GetOperationToSignAsync(
            string operationId,
            CancellationToken cancellationToken = default)
        {
            Log($"Retrieving operation({operationId})");
            try
            {
                var res = await OperationApi.GetOperationAsync(operationId, cancellationToken);
                return new BeamResult<CommonOperationResponse>(res);
            }
            catch (ApiException e)
            {
                if (e.ErrorCode == 404)
                {
                    Log($"No operation({operationId}) was found, ending");
                    return new BeamResult<CommonOperationResponse>
                    {
                        Status = BeamResultType.Error,
                        Error = "Operation was not found"
                    };
                }

                Log($"Encountered an error retrieving operation({operationId}): {e.Message} {e.ErrorContent}");
                return new BeamResult<CommonOperationResponse>(e);
            }
        }

        /// <summary>
        /// Opens an external browser to sign a transaction, returns the result via callback arg.
        /// </summary>
        /// <param name="entityId">Entity Id of the User performing signing</param>
        /// <param name="commonOperationResponse">Operation from <see cref="GetOperationToSignAsync"/></param>
        /// <param name="chainId">ChainId to perform operation on. Defaults to 13337.</param>
        /// <param name="signingBy">If set to Auto, will try to use a local Session and open Browser if there is no valid Session.</param>
        /// <param name="secondsTimeout">Optional timeout in seconds, defaults to 240</param>
        /// <param name="cancellationToken">Optional CancellationToken</param>
        /// <returns>UniTask</returns>
        public async UniTask<BeamResult<CommonOperationResponse.StatusEnum>> StartOperationSigningAsync(
            string entityId,
            CommonOperationResponse commonOperationResponse,
            int chainId = Constants.DefaultChainId,
            OperationSigningBy signingBy = OperationSigningBy.Auto,
            int secondsTimeout = DefaultTimeoutInSeconds,
            CancellationToken cancellationToken = default)
        {
            if (signingBy is OperationSigningBy.Auto or OperationSigningBy.Session)
            {
                Log("Retrieving active session");
                var (activeSession, activeSessionKeyPair) =
                    await GetActiveSessionAndKeysAsync(entityId, chainId, cancellationToken);

                var hasActiveSession = activeSessionKeyPair != null && activeSession != null;
                if (hasActiveSession)
                {
                    Log($"Has an active session until: {activeSession.EndTime:o}, using it to sign the operation");
                    return await SignOperationUsingSessionAsync(commonOperationResponse, activeSessionKeyPair,
                        cancellationToken);
                }
            }

            if (signingBy is OperationSigningBy.Auto or OperationSigningBy.Browser)
            {
                Log("No active session found, using browser to sign the operation");
                return await SignOperationUsingBrowserAsync(commonOperationResponse, secondsTimeout, cancellationToken);
            }

            Log($"No active session found, {nameof(signingBy)} set to {signingBy.ToString()}");
            return new BeamResult<CommonOperationResponse.StatusEnum>
            {
                Result = CommonOperationResponse.StatusEnum.Error,
                Status = BeamResultType.Error,
                Error = $"No active session found, {nameof(signingBy)} set to {signingBy.ToString()}"
            };
        }
    }
}