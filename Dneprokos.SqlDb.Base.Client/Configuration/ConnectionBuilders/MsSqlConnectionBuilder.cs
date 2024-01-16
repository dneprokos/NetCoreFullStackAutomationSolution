using System.Data.SqlClient;

namespace Dneprokos.SqlDb.Base.Client.Configuration.ConnectionBuilders
{
    /// <summary>
    /// Helper class to build a MsSQL connection string.
    /// </summary>
    public class MsSqlConnectionBuilder
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public MsSqlConnectionBuilder()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        }

        public MsSqlConnectionBuilder(string connectionString)
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        #region Builder methods

        public MsSqlConnectionBuilder WithDataSource(string dataSource)
        {
            _sqlConnectionStringBuilder.DataSource = dataSource;
            return this;
        }

        public MsSqlConnectionBuilder WithInitialCatalog(string initialCatalog)
        {
            _sqlConnectionStringBuilder.InitialCatalog = initialCatalog;
            return this;
        }

        public MsSqlConnectionBuilder WithUserId(string userId)
        {
            _sqlConnectionStringBuilder.UserID = userId;
            return this;
        }

        public MsSqlConnectionBuilder WithPassword(string password)
        {
            _sqlConnectionStringBuilder.Password = password;
            return this;
        }

        public MsSqlConnectionBuilder WithConnectTimeout(int connectTimeout)
        {
            _sqlConnectionStringBuilder.ConnectTimeout = connectTimeout;
            return this;
        }

        public MsSqlConnectionBuilder WithIntegratedSecurity(bool integratedSecurity)
        {
            _sqlConnectionStringBuilder.IntegratedSecurity = integratedSecurity;
            return this;
        }

        public MsSqlConnectionBuilder WithMultipleActiveResultSets(bool multipleActiveResultSets)
        {
            _sqlConnectionStringBuilder.MultipleActiveResultSets = multipleActiveResultSets;
            return this;
        }

        public MsSqlConnectionBuilder WithEncrypt(bool encrypt)
        {
            _sqlConnectionStringBuilder.Encrypt = encrypt;
            return this;
        }

        public MsSqlConnectionBuilder WithTrustServerCertificate(bool trustServerCertificate)
        {
            _sqlConnectionStringBuilder.TrustServerCertificate = trustServerCertificate;
            return this;
        }

        public string Build()
        {
            return _sqlConnectionStringBuilder.ConnectionString;
        }

        #endregion
    }
}
