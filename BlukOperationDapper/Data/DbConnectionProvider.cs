using BlukOperationDapper.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace BlukOperationDapper.Data
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly AppSettings _options;

        public DbConnectionProvider(IOptions<AppSettings> options)
        {
            _options = options.Value;
        }
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_options.SQLConnectionString);
            connection.Open();
            return connection;
        }
    }
}
