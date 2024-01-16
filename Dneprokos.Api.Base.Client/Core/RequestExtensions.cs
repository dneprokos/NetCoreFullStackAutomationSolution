using Newtonsoft.Json;
using RestSharp;

namespace Dneprokos.Api.Base.Client.Core
{
    public static class RequestExtensions
    {
        public static T? DeserializeResponse<T>(this RestResponse response)
        {
            string jsonResponse = response.Content!;
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
