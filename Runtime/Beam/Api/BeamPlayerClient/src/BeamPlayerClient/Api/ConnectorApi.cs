/*
 * Player API
 *
 * The Player API is a service to integrate your game with Beam
 *
 * The version of the OpenAPI document: 1.0.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using BeamPlayerClient.Client;
using BeamPlayerClient.Model;

namespace BeamPlayerClient.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IConnectorApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <returns>GenerateConnectionRequestResponse</returns>
        GenerateConnectionRequestResponse CreateConnectionRequest(GenerateConnectionRequestInput generateConnectionRequestInput);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <returns>ApiResponse of GenerateConnectionRequestResponse</returns>
        ApiResponse<GenerateConnectionRequestResponse> CreateConnectionRequestWithHttpInfo(GenerateConnectionRequestInput generateConnectionRequestInput);
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <returns>WebConnectionRequestResponse</returns>
        WebConnectionRequestResponse GenerateWebConnection(WebConnectionRequestInput webConnectionRequestInput);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <returns>ApiResponse of WebConnectionRequestResponse</returns>
        ApiResponse<WebConnectionRequestResponse> GenerateWebConnectionWithHttpInfo(WebConnectionRequestInput webConnectionRequestInput);
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <returns>GetConnectionRequestResponse</returns>
        GetConnectionRequestResponse GetConnectionRequest(string requestId);

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <returns>ApiResponse of GetConnectionRequestResponse</returns>
        ApiResponse<GetConnectionRequestResponse> GetConnectionRequestWithHttpInfo(string requestId);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IConnectorApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GenerateConnectionRequestResponse</returns>
        Cysharp.Threading.Tasks.UniTask<GenerateConnectionRequestResponse> CreateConnectionRequestAsync(GenerateConnectionRequestInput generateConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GenerateConnectionRequestResponse)</returns>
        Cysharp.Threading.Tasks.UniTask<ApiResponse<GenerateConnectionRequestResponse>> CreateConnectionRequestWithHttpInfoAsync(GenerateConnectionRequestInput generateConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of WebConnectionRequestResponse</returns>
        Cysharp.Threading.Tasks.UniTask<WebConnectionRequestResponse> GenerateWebConnectionAsync(WebConnectionRequestInput webConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (WebConnectionRequestResponse)</returns>
        Cysharp.Threading.Tasks.UniTask<ApiResponse<WebConnectionRequestResponse>> GenerateWebConnectionWithHttpInfoAsync(WebConnectionRequestInput webConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetConnectionRequestResponse</returns>
        Cysharp.Threading.Tasks.UniTask<GetConnectionRequestResponse> GetConnectionRequestAsync(string requestId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetConnectionRequestResponse)</returns>
        Cysharp.Threading.Tasks.UniTask<ApiResponse<GetConnectionRequestResponse>> GetConnectionRequestWithHttpInfoAsync(string requestId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IConnectorApi : IConnectorApiSync, IConnectorApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ConnectorApi : IDisposable, IConnectorApi
    {
        private BeamPlayerClient.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <returns></returns>
        public ConnectorApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="basePath">The target service's base path in URL format.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public ConnectorApi(string basePath)
        {
            this.Configuration = BeamPlayerClient.Client.Configuration.MergeConfigurations(
                BeamPlayerClient.Client.GlobalConfiguration.Instance,
                new BeamPlayerClient.Client.Configuration { BasePath = basePath }
            );
            this.ApiClient = new BeamPlayerClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client =  this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            this.ExceptionFactory = BeamPlayerClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorApi"/> class using Configuration object.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="configuration">An instance of Configuration.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public ConnectorApi(BeamPlayerClient.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = BeamPlayerClient.Client.Configuration.MergeConfigurations(
                BeamPlayerClient.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.ApiClient = new BeamPlayerClient.Client.ApiClient(this.Configuration.BasePath);
            this.Client = this.ApiClient;
            this.AsynchronousClient = this.ApiClient;
            ExceptionFactory = BeamPlayerClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConnectorApi(BeamPlayerClient.Client.ISynchronousClient client, BeamPlayerClient.Client.IAsynchronousClient asyncClient, BeamPlayerClient.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = BeamPlayerClient.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Disposes resources if they were created by us
        /// </summary>
        public void Dispose()
        {
            this.ApiClient?.Dispose();
        }

        /// <summary>
        /// Holds the ApiClient if created
        /// </summary>
        public BeamPlayerClient.Client.ApiClient ApiClient { get; set; } = null;

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public BeamPlayerClient.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public BeamPlayerClient.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public BeamPlayerClient.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public BeamPlayerClient.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <returns>GenerateConnectionRequestResponse</returns>
        public GenerateConnectionRequestResponse CreateConnectionRequest(GenerateConnectionRequestInput generateConnectionRequestInput)
        {
            BeamPlayerClient.Client.ApiResponse<GenerateConnectionRequestResponse> localVarResponse = CreateConnectionRequestWithHttpInfo(generateConnectionRequestInput);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <returns>ApiResponse of GenerateConnectionRequestResponse</returns>
        public BeamPlayerClient.Client.ApiResponse<GenerateConnectionRequestResponse> CreateConnectionRequestWithHttpInfo(GenerateConnectionRequestInput generateConnectionRequestInput)
        {
            // verify the required parameter 'generateConnectionRequestInput' is set
            if (generateConnectionRequestInput == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'generateConnectionRequestInput' when calling ConnectorApi->CreateConnectionRequest");

            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = generateConnectionRequestInput;

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<GenerateConnectionRequestResponse>("/v1/player/connector", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateConnectionRequest", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GenerateConnectionRequestResponse</returns>
        public async Cysharp.Threading.Tasks.UniTask<GenerateConnectionRequestResponse> CreateConnectionRequestAsync(GenerateConnectionRequestInput generateConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var task = CreateConnectionRequestWithHttpInfoAsync(generateConnectionRequestInput, cancellationToken);
            BeamPlayerClient.Client.ApiResponse<GenerateConnectionRequestResponse> localVarResponse = await task;
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="generateConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GenerateConnectionRequestResponse)</returns>
        public async Cysharp.Threading.Tasks.UniTask<BeamPlayerClient.Client.ApiResponse<GenerateConnectionRequestResponse>> CreateConnectionRequestWithHttpInfoAsync(GenerateConnectionRequestInput generateConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'generateConnectionRequestInput' is set
            if (generateConnectionRequestInput == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'generateConnectionRequestInput' when calling ConnectorApi->CreateConnectionRequest");


            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };


            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = generateConnectionRequestInput;

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request

            var task = this.AsynchronousClient.PostAsync<GenerateConnectionRequestResponse>("/v1/player/connector", localVarRequestOptions, this.Configuration, cancellationToken);

            var localVarResponse = await task;

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateConnectionRequest", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <returns>WebConnectionRequestResponse</returns>
        public WebConnectionRequestResponse GenerateWebConnection(WebConnectionRequestInput webConnectionRequestInput)
        {
            BeamPlayerClient.Client.ApiResponse<WebConnectionRequestResponse> localVarResponse = GenerateWebConnectionWithHttpInfo(webConnectionRequestInput);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <returns>ApiResponse of WebConnectionRequestResponse</returns>
        public BeamPlayerClient.Client.ApiResponse<WebConnectionRequestResponse> GenerateWebConnectionWithHttpInfo(WebConnectionRequestInput webConnectionRequestInput)
        {
            // verify the required parameter 'webConnectionRequestInput' is set
            if (webConnectionRequestInput == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'webConnectionRequestInput' when calling ConnectorApi->GenerateWebConnection");

            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = webConnectionRequestInput;

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<WebConnectionRequestResponse>("/v1/player/connector/web-connection", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateWebConnection", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of WebConnectionRequestResponse</returns>
        public async Cysharp.Threading.Tasks.UniTask<WebConnectionRequestResponse> GenerateWebConnectionAsync(WebConnectionRequestInput webConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var task = GenerateWebConnectionWithHttpInfoAsync(webConnectionRequestInput, cancellationToken);
            BeamPlayerClient.Client.ApiResponse<WebConnectionRequestResponse> localVarResponse = await task;
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="webConnectionRequestInput"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (WebConnectionRequestResponse)</returns>
        public async Cysharp.Threading.Tasks.UniTask<BeamPlayerClient.Client.ApiResponse<WebConnectionRequestResponse>> GenerateWebConnectionWithHttpInfoAsync(WebConnectionRequestInput webConnectionRequestInput, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'webConnectionRequestInput' is set
            if (webConnectionRequestInput == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'webConnectionRequestInput' when calling ConnectorApi->GenerateWebConnection");


            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };


            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = webConnectionRequestInput;

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request

            var task = this.AsynchronousClient.PostAsync<WebConnectionRequestResponse>("/v1/player/connector/web-connection", localVarRequestOptions, this.Configuration, cancellationToken);

            var localVarResponse = await task;

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateWebConnection", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <returns>GetConnectionRequestResponse</returns>
        public GetConnectionRequestResponse GetConnectionRequest(string requestId)
        {
            BeamPlayerClient.Client.ApiResponse<GetConnectionRequestResponse> localVarResponse = GetConnectionRequestWithHttpInfo(requestId);
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <returns>ApiResponse of GetConnectionRequestResponse</returns>
        public BeamPlayerClient.Client.ApiResponse<GetConnectionRequestResponse> GetConnectionRequestWithHttpInfo(string requestId)
        {
            // verify the required parameter 'requestId' is set
            if (requestId == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'requestId' when calling ConnectorApi->GetConnectionRequest");

            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("requestId", BeamPlayerClient.Client.ClientUtils.ParameterToString(requestId)); // path parameter

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetConnectionRequestResponse>("/v1/player/connector/{requestId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetConnectionRequest", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetConnectionRequestResponse</returns>
        public async Cysharp.Threading.Tasks.UniTask<GetConnectionRequestResponse> GetConnectionRequestAsync(string requestId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var task = GetConnectionRequestWithHttpInfoAsync(requestId, cancellationToken);
            BeamPlayerClient.Client.ApiResponse<GetConnectionRequestResponse> localVarResponse = await task;
            return localVarResponse.Data;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="requestId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetConnectionRequestResponse)</returns>
        public async Cysharp.Threading.Tasks.UniTask<BeamPlayerClient.Client.ApiResponse<GetConnectionRequestResponse>> GetConnectionRequestWithHttpInfoAsync(string requestId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'requestId' is set
            if (requestId == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'requestId' when calling ConnectorApi->GetConnectionRequest");


            BeamPlayerClient.Client.RequestOptions localVarRequestOptions = new BeamPlayerClient.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };


            var localVarContentType = BeamPlayerClient.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = BeamPlayerClient.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("requestId", BeamPlayerClient.Client.ClientUtils.ParameterToString(requestId)); // path parameter

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request

            var task = this.AsynchronousClient.GetAsync<GetConnectionRequestResponse>("/v1/player/connector/{requestId}", localVarRequestOptions, this.Configuration, cancellationToken);

            var localVarResponse = await task;

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetConnectionRequest", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
