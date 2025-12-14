using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Postgres.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Создаем отдельный метод для регистрации DataAccess слоя в DI
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<INotesStorage, NotesStorage>();
            services.AddSingleton<IUserStorage, UserStorage>();
            services.AddSingleton<DapperContext>();
            services.Configure<DatabaseSettings>(x => x.ConnectionString = connectionString);

            return services;
        }
    }
}
