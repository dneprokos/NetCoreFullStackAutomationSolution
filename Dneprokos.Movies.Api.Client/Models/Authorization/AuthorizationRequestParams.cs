namespace Dneprokos.Movies.Api.Client.Models.Authorization
{
    public class AuthorizationRequestParams
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public AuthorizationRequestParams()
        {
        }

        public AuthorizationRequestParams(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
