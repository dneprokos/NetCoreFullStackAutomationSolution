using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace Dneprokos.Api.Base.Client.Core
{
    public class BaseApiClient
    {
        private readonly RestClient restClient;
        private RestRequest request;
        public ILogger? Log { get; set; }

        #region Constructors

        /// <summary>
        /// Initialize Base API Client
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public BaseApiClient(ILogger? logger = null)
        {
            restClient = new RestClient();
            Log = logger;
        }

        /// <summary>
        /// Initialize Base API Client
        /// </summary>
        /// <param name="options">Rest Client Options</param>
        /// <param name="logger">Logger instance</param>
        public BaseApiClient(RestClientOptions options, ILogger? logger = null)
        {
            restClient = new RestClient(options);
            Log = logger;
        }

        /// <summary>
        /// Initialize Base API Client
        /// </summary>
        /// <param name="baseUrl">Base URL</param>
        /// <param name="method">HTTP Method</param>
        /// <param name="logger">Logger instance</param>
        public BaseApiClient(string baseUrl, ILogger? logger = null)
        {
            var options = new RestClientOptions(baseUrl);
            restClient = new RestClient(options);
            Log = logger;
        }

        public BaseApiClient(string baseUrl, IAuthenticator authentication, ILogger? logger = null)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = authentication,
            };
            restClient = new RestClient(options);
            Log = logger;
        }

        #endregion

        #region Request builder

        /// <summary>
        /// Initialize RestRequest
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="method">Method</param>
        /// <returns></returns>
        public BaseApiClient AddRequest(string resourcePath, Method method)
        {
            request = new RestRequest(resourcePath, method);
            return this;
        }

        /// <summary>
        /// Initialize RestRequest
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <param name="method">Method</param>
        /// <returns></returns>
        public BaseApiClient AddRequest(string resourcePath, IAuthenticator authenticator, Method method)
        {
            request = new RestRequest(resourcePath, method)
            {
                Authenticator = authenticator
            };
            return this;
        }

        /// <summary>
        /// Initialize Request with POST method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UsePostMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Post);
        }

        /// <summary>
        /// Initialize Request with POST method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UsePostMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Post);
        }

        /// <summary>
        /// Initializes Request with GET method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UseGetMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Get);
        }

        /// <summary>
        /// Initializes Request with GET method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UseGetMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Get);
        }

        /// <summary>
        /// Initialize Request with PUT method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UsePutMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Put);
        }

        /// <summary>
        /// Initialize Request with PUT method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UsePutMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Put);
        }

        /// <summary>
        /// Initialize Request with DELETE method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UseDeleteMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Delete);
        }

        /// <summary>
        /// Initialize Request with DELETE method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UseDeleteMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Delete);
        }

        /// <summary>
        /// Initialize Request with PATCH method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UsePatchMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Patch);
        }

        /// <summary>
        /// Initialize Request with PATCH method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UsePatchMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Patch);
        }

        /// <summary>
        /// Initialize Request with SEARCH method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UseSearchMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Search);
        }

        /// <summary>
        /// Initialize Request with SEARCH method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UseSearchMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Search);
        }

        /// <summary>
        /// Initialize Request with COPY method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <returns></returns>
        public BaseApiClient UseCopyMethod(string resourcePath)
        {
            return AddRequest(resourcePath, Method.Copy);
        }

        /// <summary>
        /// Initialize Request with COPY method
        /// </summary>
        /// <param name="resourcePath">Resource url path</param>
        /// <param name="authenticator">Authentication</param>
        /// <returns></returns>
        public BaseApiClient UseCopyMethod(string resourcePath, IAuthenticator authenticator)
        {
            return AddRequest(resourcePath, authenticator, Method.Copy);
        }

        #endregion

        #region Headers

        /// <summary>
        /// Adds header
        /// </summary>
        /// <param name="header">Header KeyValuePair</param>
        /// <returns></returns>
        public BaseApiClient AddHeader(KeyValuePair<string, string> header)
        {
            restClient.AddDefaultHeader(header.Key, header.Value);
            return this;
        }

        /// <summary>
        /// Adds header
        /// </summary>
        /// <param name="key">Header key</param>
        /// <param name="value">Header value</param>
        /// <returns></returns>
        public BaseApiClient AddHeader(string key, string value)
        {
            restClient.AddDefaultHeader(key, value);
            return this;
        }

        /// <summary>
        /// Adds header
        /// </summary>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        public BaseApiClient AddHeaders(Dictionary<string, string> headers)
        {
            restClient.AddDefaultHeaders(headers);
            return this;
        }

        #endregion

        #region Cookies

        /// <summary>
        /// Adds cookies
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Value</param>
        /// <param name="path">Path</param>
        /// <param name="domain">Domain</param>
        /// <returns></returns>
        public BaseApiClient AddCookie(string name, string value, string path, string domain)
        {
            request.AddCookie(name, value, path, domain);
            return this;
        }

        /// <summary>
        /// Adds cookies
        /// </summary>
        /// <param name="cookieContainer">Container with cookies</param>
        /// <returns></returns>
        public BaseApiClient AddCookies(CookieContainer cookieContainer)
        {
            request.CookieContainer = cookieContainer;
            return this;
        }

        #endregion

        #region Method

        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="method">Http method</param>
        /// <returns></returns>
        public BaseApiClient AddMethod(Method method)
        {
            request.Method = method;
            return this;
        }

        #endregion

        #region Body

        /// <summary>
        /// Adds body
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public BaseApiClient AddBody(string queryString)
        {
            request.AddBody(new { query = queryString });
            return this;
        }

        /// <summary>
        /// Adds body
        /// </summary>
        /// <typeparam name="T">ObjectType</typeparam>
        /// <param name="model">Object to pass</param>
        /// <returns></returns>
        public BaseApiClient AddBody<T>(T model)
        {
            string jsonBody = JsonConvert.SerializeObject(model);
            request.AddBody(jsonBody, "application/json");
            return this;
        }

        #endregion

        #region Send request

        /// <summary>
        /// Sends prepared request
        /// </summary>
        /// <returns></returns>
        public RestResponse SendRequest()
        {
            RestResponse response = restClient.Execute(request);
            PerformRequestLog(request, response);

            return response;
        }

        /// <summary>
        /// Sends prepared request
        /// </summary>
        /// <param name="bodyString">String body</param>
        /// <returns></returns>
        public RestResponse SendRequest(string bodyString)
        {
            request.AddBody(new { query = bodyString });
            RestResponse response = restClient.Execute(request);
            PerformRequestLog(request, response);

            return response;
        }

        private void PerformRequestLog(RestRequest restRequest, RestResponse restResponse)
        {
            if (Log != null)
            {
                //Request builder
                var requestLog = new
                {
                    uri = restClient.BuildUri(restRequest),
                    resource = restRequest.Resource,
                    method = restRequest.Method.ToString(),
                    parameters = restRequest.Parameters.Select(parameter => new
                    {
                        name = parameter.Name,
                        contentType = parameter.ContentType.ToString(),
                        type = parameter.Type.ToString(),
                        value = parameter.Name!.Equals("Authorization") ?
                        "*************" :
                            (parameter.Type.ToString().Equals("RequestBody") &&
                                parameter.ContentType.ToString().Equals(ContentType.Json)) ?
                                ConvertJsonString(parameter.Value) :
                            parameter.Value
                    })
                };


                //Response builder
                var responseLog = new
                {
                    responseUri = restResponse.ResponseUri,
                    statusCode = restResponse.StatusCode,
                    contentType = restResponse.ContentType,
                    content = restResponse.ContentType == ContentType.Json ?
                        JsonConvert.DeserializeObject(restResponse.Content!) :
                        restResponse.Content,
                    headers = restResponse.Headers,
                    errors = restResponse.ErrorMessage
                };


                //Serialize and send
                string formattedRequesLog = JsonConvert.SerializeObject(requestLog, Formatting.Indented);
                string formattedResponseLog = JsonConvert.SerializeObject(responseLog, Formatting.Indented);

                Log.LogDebug("-------------Request------------ ");
                Log.LogDebug(formattedRequesLog);
                Log.LogDebug("-------------Response------------ ");
                Log.LogDebug($"{formattedResponseLog}");
            }
        }

        private string ConvertJsonString(object? jsonValue)
        {
            if (jsonValue == null)
            {
                return string.Empty;
            }

            var jToken = JToken.FromObject(jsonValue);

            if (jToken["query"] == null)
            {
                return string.Empty;
            }

            var queryValue = jToken["query"].Value<string>();

            if (string.IsNullOrEmpty(queryValue))
            {
                return string.Empty;
            }

            // Remove leading and trailing whitespaces, including line breaks
            queryValue = queryValue.Trim();

            // Replace escaped newline characters with actual line breaks
            queryValue = queryValue
                .Replace("\\r\\n", Environment.NewLine);

            return queryValue;
        }

        #endregion
    }
}
