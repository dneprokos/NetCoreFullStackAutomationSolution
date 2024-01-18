namespace Dneprokos.Api.Base.Client.Authorizations
{
    public class BasicAuthorization
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BasicAuthorization()
        {
        }

        public BasicAuthorization(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
