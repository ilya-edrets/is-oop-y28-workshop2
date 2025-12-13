using Core.Abstractions;
using Core.Entities;
using Dapper;
using System.Threading.Tasks;

namespace DataAccess.Postgres
{
    internal class UserStorage : IUserStorage
    {
        private DapperContext _dapperContext;

        public UserStorage(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddUser(User user)
        {
            var query = """
                INSERT INTO users (name, password_hash)
                VALUES (@Name, @PasswordHash);
                """;

            var parameters = new DynamicParameters();
            parameters.Add("Name", user.Name);
            parameters.Add("PasswordHash", user.PasswordHash);

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<User?> FindUser(string name)
        {
            var query = """
                SELECT name, password_hash
                FROM users
                WHERE name = @Name;
                """;

            var parameters = new DynamicParameters();
            parameters.Add("Name", name);

            using var connection = _dapperContext.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, parameters);

            return user;
        }
    }
}
