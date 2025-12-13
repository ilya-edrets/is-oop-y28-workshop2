using DbUp;
using System.Reflection;

namespace DataAccess.Postgres.Extensions
{
    public static class DatabaseExtension
    {
        public static bool MigrateDatabase(string connectionString)
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            return result.Successful;
        }
    }
}
