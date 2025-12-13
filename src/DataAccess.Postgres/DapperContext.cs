using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace DataAccess.Postgres
{
    internal class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IOptions<DatabaseSettings> options)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            _connectionString = options.Value.ConnectionString;
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
