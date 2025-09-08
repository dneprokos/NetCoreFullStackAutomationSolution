namespace Dneprokos.Movies.Api.Client.Models.Movies
{
    public class MovieApiModelBuilder
    {
        private readonly MovieApiModel _movie = new();

        public MovieApiModelBuilder WithId(int? id)
        {
            _movie.Id = id;
            return this;
        }

        public MovieApiModelBuilder WithName(string? name)
        {
            _movie.Name = name;
            return this;
        }

        public MovieApiModelBuilder WithReleaseDate(int? releaseDate)
        {
            _movie.ReleaseDate = releaseDate;
            return this;
        }

        public MovieApiModelBuilder WithGenreIds(List<int?>? genreIds)
        {
            _movie.GenreIds = genreIds;
            return this;
        }

        public MovieApiModelBuilder AddGenreId(int? genreId)
        {
            _movie.GenreIds ??= new List<int?>();
            _movie.GenreIds.Add(genreId);
            return this;
        }

        public MovieApiModel Build()
        {
            return _movie;
        }
    }
}
