using Oracle.ManagedDataAccess.Client;

namespace Dneprokos.SqlDb.Base.Client.Configuration.ConnectionBuilders
{
    public class OracleConnectionBuilder
    {
        private readonly OracleConnectionStringBuilder _oracleConnectionStringBuilder;

        public OracleConnectionBuilder()
        {
            _oracleConnectionStringBuilder = new OracleConnectionStringBuilder();
        }

        public OracleConnectionBuilder(string connectionString)
        {
            _oracleConnectionStringBuilder = new OracleConnectionStringBuilder(connectionString);
        }

        #region Builder methods

        public OracleConnectionBuilder WithDataSource(string dataSource)
        {
            _oracleConnectionStringBuilder.DataSource = dataSource;
            return this;
        }

        public OracleConnectionBuilder WithUserID(string userId)
        {
            _oracleConnectionStringBuilder.UserID = userId;
            return this;
        }

        public OracleConnectionBuilder WithPassword(string password)
        {
            _oracleConnectionStringBuilder.Password = password;
            return this;
        }

        public OracleConnectionBuilder WithConnectionTimeout(int connectTimeout)
        {
            _oracleConnectionStringBuilder.ConnectionTimeout = connectTimeout;
            return this;
        }

        public OracleConnectionBuilder WithPersistSecurityInfo(bool integratedSecurity)
        {
            _oracleConnectionStringBuilder.PersistSecurityInfo = integratedSecurity;
            return this;
        }

        // Additional Oracle-specific settings can be added here

        public string Build()
        {
            return _oracleConnectionStringBuilder.ConnectionString;
        }

        #endregion
    }
}
