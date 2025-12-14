using DbUp;
using System.Reflection;

namespace DataAccess.Postgres.Helpers
{
    public static class DatabaseHelper
    {
        // Запускает выполнение всех скриптов из папки SqlScripts если они еще не были выполнены на указанной БД
        // Метод нужно вызвать до запуска веб-сервиса, т.е. где-то рядом с методом Main
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
