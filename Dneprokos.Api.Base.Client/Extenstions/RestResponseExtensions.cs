using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Dneprokos.Api.Base.Client.Extenstions
{
    public static class RestResponseExtensions
    {
        /// <summary>
        /// Verifies if response status code it equal to expected
        /// </summary>
        /// <param name="response">Response</param>
        /// <param name="expectedStatusCode">Expected status code</param>
        /// <returns>RestResponse</returns>
        public static RestResponse VerifyStatusCodeIsEqualTo(
            this RestResponse response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode);
            return response;
        }

        /// <summary>
        /// Verifies if response status code it equal to expected
        /// </summary>
        /// <param name="response">Response</param>
        /// <param name="expectedStatusCode">Expected status code</param>
        /// <returns>RestResponse</returns>
        public static RestResponse VerifyStatusCodeIsEqualTo(
            this RestResponse response, int expectedStatusCode)
        {
            ((int)response.StatusCode).Should().Be(expectedStatusCode); 
            return response;
        }

        /// <summary>
        /// Deserializes RestResponse Content
        /// </summary>
        /// <typeparam name="T">Expected model</typeparam>
        /// <param name="response">Response</param>
        /// <param name="settings">JsonSerializationSettings. Default: Null</param>
        /// <returns></returns>
        public static T? ConvertJsonToModel<T>(
            this RestResponse response, JsonSerializerSettings? settings = null)
        {
            string jsonResponse = response.Content!;
            return settings == null ? 
                JsonConvert.DeserializeObject<T>(jsonResponse) :
                JsonConvert.DeserializeObject<T>(jsonResponse, settings);
        }
    }
}
