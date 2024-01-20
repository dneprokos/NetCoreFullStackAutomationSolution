namespace Dneprokos.Movies.Api.Client.Data
{
    public class MoviesApiConstants
    {
        public const int DefautPageNumber = 1;

        public const int DefautPageLimit = 10;

        public const string NoUserPermissionsMessage = "Your user role cannot perform this operation";

        public const string NameLessSymbolsMesssage = "\"name\" length must be at least 3 characters long";

        public const string PageLessThanMinMessage = "\"page\" must be larger than or equal to 1";

        public const string LimitLessThanMinMessage = "\"limit\" must be larger than or equal to 1";
    }
}
