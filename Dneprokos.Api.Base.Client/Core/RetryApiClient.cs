using RestSharp;
using RestSharp.Authenticators;

namespace Dneprokos.Api.Base.Client.Core
{
    public class RetryApiClient : IApiClient
    {
        private readonly IApiClient _client;
        private readonly int _maxRetries;

        public RetryApiClient(IApiClient client, int maxRetries = 3)
        {
            _client = client;
            _maxRetries = maxRetries;
        }

        public RestResponse SendRequest()
        {
            for (int i = 0; i < _maxRetries; i++)
            {
                try
                {
                    return _client.SendRequest();
                }
                catch (Exception) when (i < _maxRetries - 1)
                {
                    Thread.Sleep(1000 * (i + 1)); // Exponential backoff
                }
            }
            throw new Exception("Max retries exceeded");
        }

        public RestResponse SendRequest(string bodyString)
        {
            for (int i = 0; i < _maxRetries; i++)
            {
                try
                {
                    return _client.SendRequest(bodyString);
                }
                catch (Exception) when (i < _maxRetries - 1)
                {
                    Thread.Sleep(1000 * (i + 1)); // Exponential backoff
                }
            }
            throw new Exception("Max retries exceeded");
        }

        public IApiClient AddBody(string queryString)
        {
            return _client.AddBody(queryString);
        }

        public IApiClient AddBody<T>(T model)
        {
            return _client.AddBody<T>(model);
        }

        public IApiClient AddHeader(KeyValuePair<string, string> header)
        {
            return _client.AddHeader(header);
        }

        public IApiClient AddHeaders(Dictionary<string, string> headers)
        {
            return _client.AddHeaders(headers);
        }

        public IApiClient AddQueryParameter(string name, string value)
        {
            return _client.AddQueryParameter(name, value);
        }

        public IApiClient AddQueryParameters(Dictionary<string, string> parameters)
        {
            return _client.AddQueryParameters(parameters);
        }

        public IApiClient AddRequest(string resourcePath, Method method)
        {
            return _client.AddRequest(resourcePath, method);
        }

        public IApiClient AddRequest(string resourcePath, IAuthenticator authenticator, Method method)
        {
            return _client.AddRequest(resourcePath, authenticator, method);
        }

        

        public IApiClient UseDeleteMethod(string resourcePath)
        {
            return _client.UseDeleteMethod(resourcePath);
        }

        public IApiClient UseGetMethod(string resourcePath)
        {
            return _client.UseGetMethod(resourcePath);
        }

        public IApiClient UsePostMethod(string resourcePath)
        {
            return _client.UsePostMethod(resourcePath);
        }

        public IApiClient UsePutMethod(string resourcePath)
        {
            return _client.UsePutMethod(resourcePath);
        }
    }
}
