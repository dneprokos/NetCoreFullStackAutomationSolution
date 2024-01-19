namespace Dneprokos.Movies.Api.Client.Data
{
    public class MoviesEndpointsUrls
    {
        public static string Authorization() => "/authorization";

        public static string Users() => "/users";

        public static string Genres() => "/genres";

        public static string GenresBulk() => "genres/bulk";

        public static string Genres(int id) => $"/genres/{id}";

        public static string Movies() => "/movies";

        public static string Movies(int id) => $"/movies/{id}";
    }
}
