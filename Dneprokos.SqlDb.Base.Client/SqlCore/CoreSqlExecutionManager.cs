using Dneprokos.SqlDb.Base.Client.Constants;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Data;
using System.Reflection;
using Dapper;
using FluentAssertions;
using ConsoleTables;
using Dneprokos.SqlDb.Base.Client.SqlCore.Factory;

namespace Dneprokos.SqlDb.Base.Client.SqlCore
{
    public class CoreSqlExecutionManager
    {
        /// <summary>
        /// Max result rows to display in log
        /// </summary>
        public int MaxRowsToDisplayInLogger { get; set; }

        /// <summary>
        /// Logger instance
        /// </summary>
        public ILogger? Logger { get; set; }

        private readonly string _connectionString;
        private const string NoColumnLabel = "UNDEFINED NAME";
        private readonly SupportedSqlProviders _provider;

        #region Constructors

        public CoreSqlExecutionManager(SupportedSqlProviders provider, string connectionString)
        {
            _connectionString = connectionString;
            MaxRowsToDisplayInLogger = SqlClientConstants.MaxRowsToDisplayInLogger;
            _provider = provider;
        }

        public CoreSqlExecutionManager(SupportedSqlProviders provider, string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            MaxRowsToDisplayInLogger = SqlClientConstants.MaxRowsToDisplayInLogger;
            Logger = logger;
            _provider = provider;
        }

        public CoreSqlExecutionManager(SupportedSqlProviders provider, string connectionString, int maxRowsToDisplay)
        {
            _connectionString = connectionString;
            MaxRowsToDisplayInLogger = maxRowsToDisplay;
            _provider = provider;
        }

        public CoreSqlExecutionManager(SupportedSqlProviders provider, string connectionString, ILogger logger, int maxRowsToDisplay)
        {
            _connectionString = connectionString;
            MaxRowsToDisplayInLogger = maxRowsToDisplay;
            Logger = logger;
            _provider = provider;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute SQL query, return result as IEnumerable of generic objects. Possible empty list.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string? sql, object? param = null, IDbTransaction? transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider!, _connectionString!).DbConnection!;
            var result = dbConnection.Query<T>(sql!, param, transaction, buffered, commandTimeout, commandType);

            var convertedResult = result as T[] ?? result.ToArray();

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {
                if (convertedResult == null)
                    LogDebugNullableResult();
                else
                {

                    ExecuteFunctionInTryCatch(() =>
                    {
                        if (convertedResult.Length > 0)
                        {
                            Type datatype = convertedResult.First()!.GetType();
                            bool isValueType = datatype.IsValueType;

                            if (isValueType || datatype == typeof(string))
                            {
                                PrintTableForPrimitiveType(convertedResult);
                            }
                            else
                            {
                                PrintTableForReferenceType(convertedResult, datatype);
                            }
                        }
                    });
                }
            }

            return convertedResult!;
        }

        /// <summary>
        /// Execute SQL query, return first result or fail if no result.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">SQL query</param>
        /// <returns></returns>
        public T ExecuteQueryFirst<T>(string? sql)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider, _connectionString).DbConnection;
            T result = dbConnection!.QueryFirst<T>(sql!);

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {
                if (result == null)
                    LogDebugNullableResult();
                else
                {
                    ExecuteFunctionInTryCatch(() =>
                    {
                        Type dataType = result!.GetType();
                        bool isValueType = dataType.IsValueType;

                        if (isValueType || dataType == typeof(string))
                        {
                            LogTableForPrimitiveType(result);
                        }
                        else
                        {
                            PrintTableForReferenceType(result, dataType);
                        }
                    });
                }
            }

            return result!;
        }

        /// <summary>
        /// Execute SQL query, return first result or default value of specified type if no result.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">SQL query</param>
        /// <returns></returns>
        public T ExecuteQueryFirstOrDefault<T>(string? sql)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider, _connectionString).DbConnection;
            T result = dbConnection!.QueryFirstOrDefault<T>(sql!)!;

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {
                if (result == null)
                    LogDebugNullableResult();
                else
                {
                    ExecuteFunctionInTryCatch(() =>
                    {
                        Type dataType = result!.GetType();
                        bool isValueType = dataType.IsValueType;

                        if (isValueType || dataType == typeof(string))
                        {
                            LogTableForPrimitiveType(result);
                        }
                        else
                        {
                            PrintTableForReferenceType(result, dataType);
                        }
                    });
                }
            }

            return result!;
        }

        /// <summary>
        /// Execute SQL query, return first result or default value of specified type if no result.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">SQL query</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T ExecuteQueryFirstOrDefault<T>(string? sql, object param)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider, _connectionString).DbConnection;
            T result = dbConnection!.QueryFirstOrDefault<T>(sql!, param)!;

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {
                if (result == null)
                    LogDebugNullableResult();
                else
                {
                    ExecuteFunctionInTryCatch(() =>
                    {
                        Type dataType = result!.GetType();
                        bool isValueType = dataType.IsValueType;

                        if (isValueType || dataType == typeof(string))
                        {
                            LogTableForPrimitiveType(result);
                        }
                        else
                        {
                            PrintTableForReferenceType(result, dataType);
                        }
                    });
                }
            }

            return result!;
        }

        /// <summary>
        /// Execute SQL query, return single result or fail if no result or more than one result.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">SQL query</param>
        /// <returns></returns>
        public T ExecuteQuerySingle<T>(string? sql)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider, _connectionString).DbConnection;

            T result = dbConnection!.QuerySingle<T>(sql!);

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {
                if (result == null)
                    LogDebugNullableResult();
                else
                {
                    ExecuteFunctionInTryCatch(() =>
                    {
                        Type dataType = result!.GetType();
                        bool isValueType = dataType.IsValueType;

                        if (isValueType || dataType == typeof(string))
                        {
                            LogTableForPrimitiveType(result);
                        }
                        else
                        {
                            PrintTableForReferenceType(result, dataType);
                        }
                    });
                }
            }

            return result!;
        }

        /// <summary>
        /// Execute SQL query, return single result or default value of specified type if no result or more than one result.
        /// </summary>
        /// <typeparam name="T">Type of values to which you want to convert results</typeparam>
        /// <param name="sql">SQL query</param>
        /// <returns></returns>
        public T ExecuteQuerySingleOrDefault<T>(string? sql)
        {
            PrintSqlQuery(sql);
            using DbConnection? dbConnection = new SqlConnectionFactory(_provider, _connectionString).DbConnection;

            T result = dbConnection!.QuerySingleOrDefault<T>(sql!)!;

            if (Logger != null && MaxRowsToDisplayInLogger > 0)
            {

                if (result == null)
                    LogDebugNullableResult();
                else
                {
                    ExecuteFunctionInTryCatch(() =>
                    {
                        Type dataType = result!.GetType();
                        bool isValueType = dataType.IsValueType;

                        if (isValueType || dataType == typeof(string))
                        {
                            LogTableForPrimitiveType(result);
                        }
                        else
                        {
                            PrintTableForReferenceType(result, dataType);
                        }
                    });
                }
            }

            return result!;
        }

        #endregion

        #region Private Methods

        private void LogDebugNullableResult()
        {
            Logger?.LogDebug("SQL query results: NULL");
        }

        private void PrintSqlQuery(string? sqlQuery)
        {
            sqlQuery.Should().NotBeNullOrEmpty("Sql query is null or empty");
            Logger?.LogDebug($"Execute SQL query: {sqlQuery}");
        }

        private void ExecuteFunctionInTryCatch(Action function)
        {
            try
            {
                function();
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error parsing SQL result");
                Logger?.LogError(ex.Message);
            }
        }

        private void PrintTableForPrimitiveType<T>(T[] convertedResult)
        {
            var table = new ConsoleTable(NoColumnLabel);
            foreach (var item in convertedResult)
                table.AddRow(item);

            LogDebugTableWithHint(table);
        }

        private void PrintTableForReferenceType<T>(T convertedResult, Type datatype)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(datatype.GetProperties());
            string[] propertyNames = props.Select(p => p.Name).ToArray();
            var table = new ConsoleTable(propertyNames);
            var values = new List<object>();

            foreach (var property in props)
            {
                object? propertyValue = property.GetValue(convertedResult);
                values.Add(propertyValue ?? "NULL");
            }

            table.AddRow(values.ToArray());
            LogDebugTableWithHint(table);
        }

        private void PrintTableForReferenceType<T>(T[] convertedResult, Type datatype)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(datatype.GetProperties());
            string[] propertyNames = props.Select(p => p.Name).ToArray();
            var table = new ConsoleTable(propertyNames);

            int lineNumber = 0; // To limit the number of rows to display
            for (int i = 0; i < convertedResult.Length && lineNumber < MaxRowsToDisplayInLogger; i++)
            {
                var line = convertedResult[i];
                IList<PropertyInfo> properties = new List<PropertyInfo>(datatype.GetProperties());
                var values = new List<object>();
                foreach (PropertyInfo prop in properties)
                {
                    object? propertyValue = prop.GetValue(line);
                    values.Add(propertyValue ?? "NULL");
                }

                table.AddRow(values.ToArray());
                lineNumber++;
            }

            LogDebugTableWithHint(table);
        }

        private void LogTableForPrimitiveType<T>(T convertedResult)
        {
            var table = new ConsoleTable(NoColumnLabel);
            table.AddRow(convertedResult);
            LogDebugTableWithHint(table);
        }

        private void LogDebugTableWithHint(ConsoleTable table)
        {
            var hint = $"*The number of displayed SQL table rows is limited to {MaxRowsToDisplayInLogger}. You can update 'maxRowsToDisplay' using constructor";
            Logger?.LogDebug($"SQL query results:\n{table} \n{hint}");
        }

        #endregion
    }
}
