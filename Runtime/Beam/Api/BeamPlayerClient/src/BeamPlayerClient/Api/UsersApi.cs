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
    public interface IUsersApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <returns>GetAllUsersResponse</returns>
        GetAllUsersResponse GetAllUsers(decimal? limit, decimal? offset);

        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <returns>ApiResponse of GetAllUsersResponse</returns>
        ApiResponse<GetAllUsersResponse> GetAllUsersWithHttpInfo(decimal? limit, decimal? offset);
        /// <summary>
        /// Returns a particular user
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <returns>GetUserResponse</returns>
        GetUserResponse GetUser(string entityId);

        /// <summary>
        /// Returns a particular user
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <returns>ApiResponse of GetUserResponse</returns>
        ApiResponse<GetUserResponse> GetUserWithHttpInfo(string entityId);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IUsersApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetAllUsersResponse</returns>
        System.Threading.Tasks.Task<GetAllUsersResponse> GetAllUsersAsync(decimal? limit, decimal? offset, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Returns a list of users
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetAllUsersResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetAllUsersResponse>> GetAllUsersWithHttpInfoAsync(decimal? limit, decimal? offset, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Returns a particular user
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetUserResponse</returns>
        System.Threading.Tasks.Task<GetUserResponse> GetUserAsync(string entityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Returns a particular user
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetUserResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetUserResponse>> GetUserWithHttpInfoAsync(string entityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IUsersApi : IUsersApiSync, IUsersApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class UsersApi : IDisposable, IUsersApi
    {
        private BeamPlayerClient.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <returns></returns>
        public UsersApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersApi"/> class.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="basePath">The target service's base path in URL format.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public UsersApi(string basePath)
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
        /// Initializes a new instance of the <see cref="UsersApi"/> class using Configuration object.
        /// **IMPORTANT** This will also create an instance of HttpClient, which is less than ideal.
        /// It's better to reuse the <see href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net">HttpClient and HttpClientHandler</see>.
        /// </summary>
        /// <param name="configuration">An instance of Configuration.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public UsersApi(BeamPlayerClient.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="UsersApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UsersApi(BeamPlayerClient.Client.ISynchronousClient client, BeamPlayerClient.Client.IAsynchronousClient asyncClient, BeamPlayerClient.Client.IReadableConfiguration configuration)
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
        /// Returns a list of users 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <returns>GetAllUsersResponse</returns>
        public GetAllUsersResponse GetAllUsers(decimal? limit, decimal? offset)
        {
            BeamPlayerClient.Client.ApiResponse<GetAllUsersResponse> localVarResponse = GetAllUsersWithHttpInfo(limit, offset);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Returns a list of users 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <returns>ApiResponse of GetAllUsersResponse</returns>
        public BeamPlayerClient.Client.ApiResponse<GetAllUsersResponse> GetAllUsersWithHttpInfo(decimal? limit, decimal? offset)
        {
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

            if (limit != null)
            {
                localVarRequestOptions.QueryParameters.Add(BeamPlayerClient.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if (offset != null)
            {
                localVarRequestOptions.QueryParameters.Add(BeamPlayerClient.Client.ClientUtils.ParameterToMultiMap("", "offset", offset));
            }

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetAllUsersResponse>("/v1/player/users", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAllUsers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Returns a list of users 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetAllUsersResponse</returns>
        public async System.Threading.Tasks.Task<GetAllUsersResponse> GetAllUsersAsync(decimal? limit, decimal? offset, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var task = GetAllUsersWithHttpInfoAsync(limit, offset, cancellationToken);
#if UNITY_EDITOR || !UNITY_WEBGL
            BeamPlayerClient.Client.ApiResponse<GetAllUsersResponse> localVarResponse = await task.ConfigureAwait(false);
#else
            BeamPlayerClient.Client.ApiResponse<GetAllUsersResponse> localVarResponse = await task;
#endif
            return localVarResponse.Data;
        }

        /// <summary>
        /// Returns a list of users 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="limit"> (optional)</param>
        /// <param name="offset"> (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetAllUsersResponse)</returns>
        public async System.Threading.Tasks.Task<BeamPlayerClient.Client.ApiResponse<GetAllUsersResponse>> GetAllUsersWithHttpInfoAsync(decimal? limit, decimal? offset, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

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

            if (limit != null)
            {
                localVarRequestOptions.QueryParameters.Add(BeamPlayerClient.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if (offset != null)
            {
                localVarRequestOptions.QueryParameters.Add(BeamPlayerClient.Client.ClientUtils.ParameterToMultiMap("", "offset", offset));
            }

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request

            var task = this.AsynchronousClient.GetAsync<GetAllUsersResponse>("/v1/player/users", localVarRequestOptions, this.Configuration, cancellationToken);

#if UNITY_EDITOR || !UNITY_WEBGL
            var localVarResponse = await task.ConfigureAwait(false);
#else
            var localVarResponse = await task;
#endif

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAllUsers", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Returns a particular user 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <returns>GetUserResponse</returns>
        public GetUserResponse GetUser(string entityId)
        {
            BeamPlayerClient.Client.ApiResponse<GetUserResponse> localVarResponse = GetUserWithHttpInfo(entityId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Returns a particular user 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <returns>ApiResponse of GetUserResponse</returns>
        public BeamPlayerClient.Client.ApiResponse<GetUserResponse> GetUserWithHttpInfo(string entityId)
        {
            // verify the required parameter 'entityId' is set
            if (entityId == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'entityId' when calling UsersApi->GetUser");

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

            localVarRequestOptions.PathParameters.Add("entityId", BeamPlayerClient.Client.ClientUtils.ParameterToString(entityId)); // path parameter

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetUserResponse>("/v1/player/users/{entityId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Returns a particular user 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetUserResponse</returns>
        public async System.Threading.Tasks.Task<GetUserResponse> GetUserAsync(string entityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            var task = GetUserWithHttpInfoAsync(entityId, cancellationToken);
#if UNITY_EDITOR || !UNITY_WEBGL
            BeamPlayerClient.Client.ApiResponse<GetUserResponse> localVarResponse = await task.ConfigureAwait(false);
#else
            BeamPlayerClient.Client.ApiResponse<GetUserResponse> localVarResponse = await task;
#endif
            return localVarResponse.Data;
        }

        /// <summary>
        /// Returns a particular user 
        /// </summary>
        /// <exception cref="BeamPlayerClient.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="entityId"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetUserResponse)</returns>
        public async System.Threading.Tasks.Task<BeamPlayerClient.Client.ApiResponse<GetUserResponse>> GetUserWithHttpInfoAsync(string entityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'entityId' is set
            if (entityId == null)
                throw new BeamPlayerClient.Client.ApiException(400, "Missing required parameter 'entityId' when calling UsersApi->GetUser");


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

            localVarRequestOptions.PathParameters.Add("entityId", BeamPlayerClient.Client.ClientUtils.ParameterToString(entityId)); // path parameter

            // authentication (Beam API game key) required
            if (!string.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("x-api-key")))
            {
                localVarRequestOptions.HeaderParameters.Add("x-api-key", this.Configuration.GetApiKeyWithPrefix("x-api-key"));
            }

            // make the HTTP request

            var task = this.AsynchronousClient.GetAsync<GetUserResponse>("/v1/player/users/{entityId}", localVarRequestOptions, this.Configuration, cancellationToken);

#if UNITY_EDITOR || !UNITY_WEBGL
            var localVarResponse = await task.ConfigureAwait(false);
#else
            var localVarResponse = await task;
#endif

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetUser", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
