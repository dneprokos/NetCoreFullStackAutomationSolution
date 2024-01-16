using Npgsql;

namespace Dneprokos.SqlDb.Base.Client.Configuration.ConnectionBuilders
{
    /// <summary>
    /// Helper class to build a PostgreSql connection string.
    /// </summary>
    public class PostgeSqlConnectionBuilder
    {
        private readonly NpgsqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public PostgeSqlConnectionBuilder()
        {
            _sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
        }

        public PostgeSqlConnectionBuilder(string connectionString)
        {
            _sqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        }

        #region Builder methods

        public PostgeSqlConnectionBuilder WithHost(string dataSource)
        {
            _sqlConnectionStringBuilder.Host = dataSource;
            return this;
        }

        public PostgeSqlConnectionBuilder WithPort(int port)
        {
            _sqlConnectionStringBuilder.Port = port;
            return this;
        }

        public PostgeSqlConnectionBuilder WithDatabase(string initialCatalog)
        {
            _sqlConnectionStringBuilder.Database = initialCatalog;
            return this;
        }

        public PostgeSqlConnectionBuilder WithUserId(string userId)
        {
            _sqlConnectionStringBuilder.Username = userId;
            return this;
        }

        public PostgeSqlConnectionBuilder WithPassword(string password)
        {
            _sqlConnectionStringBuilder.Password = password;
            return this;
        }

        public PostgeSqlConnectionBuilder WithTimeout(int connectTimeout)
        {
            _sqlConnectionStringBuilder.Timeout = connectTimeout;
            return this;
        }

        public string Build()
        {
            return _sqlConnectionStringBuilder.ConnectionString;
        }

        #endregion
    }
}
