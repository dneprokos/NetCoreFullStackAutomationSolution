using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace Dneprokos.Api.Base.Client.Core
{
    public class BaseApiClient
    {
        private readonly RestClient restClient;
        private readonly RestRequest request;
        public ILogger? Log { get; set; }

        #region Constructors

        public BaseApiClient(ILogger? logger = null)
        {
            restClient = new RestClient();
            request = new RestRequest() { Method = Method.Post };
            Log = logger;
        }

        public BaseApiClient(RestClientOptions options, ILogger? logger = null)
        {
            restClient = new RestClient(options);
            request = new RestRequest() { Method = Method.Post };
            Log = logger;
        }

        public BaseApiClient(string baseUrl, ILogger? logger = null)
        {
            var options = new RestClientOptions(baseUrl);
            restClient = new RestClient(options);
            request = new RestRequest() { Method = Method.Post };
            Log = logger;
        }

        #endregion

        #region Headers

        public BaseApiClient AddHeader(KeyValuePair<string, string> header)
        {
            restClient.AddDefaultHeader(header.Key, header.Value);
            return this;
        }

        public BaseApiClient AddHeader(string key, string value)
        {
            restClient.AddDefaultHeader(key, value);
            return this;
        }

        public BaseApiClient AddHeaders(Dictionary<string, string> headers)
        {
            restClient.AddDefaultHeaders(headers);
            return this;
        }

        #endregion

        #region Cookies

        public BaseApiClient AddCookie(string name, string value, string path, string domain)
        {
            request.AddCookie(name, value, path, domain);
            return this;
        }

        public BaseApiClient AddCookies(CookieContainer cookieContainer)
        {
            request.CookieContainer = cookieContainer;
            return this;
        }

        #endregion

        #region Method

        public BaseApiClient AddMethod(Method method)
        {
            request.Method = method;
            return this;
        }

        #endregion

        #region Body

        public BaseApiClient AddBody(string queryString)
        {
            request.AddBody(new { query = queryString });
            return this;
        }

        public BaseApiClient AddBody<T>(T query)
        {
            string jsonBody = JsonConvert.SerializeObject(query);
            request.AddBody(jsonBody, "application/json");
            return this;
        }

        #endregion

        #region Send request

        public RestResponse SendQueryRequest()
        {
            RestResponse response = restClient.Post(request);
            PerformRequestLog(request, response);

            return response;
        }

        public RestResponse SendQueryRequest(string queryString)
        {
            var request = new RestRequest()
            {
                Method = Method.Post
            }
            .AddBody(new { query = queryString });

            RestResponse response = restClient.Post(request);
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
