using RestSharp;
using RestSharp.Authenticators;

namespace Dneprokos.Api.Base.Client.Core
{
    public interface IApiClient
    {
        IApiClient AddRequest(string resourcePath, Method method);
        IApiClient AddRequest(string resourcePath, IAuthenticator authenticator, Method method);
        IApiClient UsePostMethod(string resourcePath);
        IApiClient UseGetMethod(string resourcePath);
        IApiClient UsePutMethod(string resourcePath);
        IApiClient UseDeleteMethod(string resourcePath);
        IApiClient AddHeader(KeyValuePair<string, string> header);
        IApiClient AddHeaders(Dictionary<string, string> headers);
        IApiClient AddBody(string queryString);
        IApiClient AddBody<T>(T model);
        IApiClient AddQueryParameter(string name, string value);
        IApiClient AddQueryParameters(Dictionary<string, string> parameters);
        RestResponse SendRequest();
        RestResponse SendRequest(string bodyString);
    }
}
