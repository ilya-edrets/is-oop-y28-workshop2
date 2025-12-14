using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Postgres.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Создаем отдельный метод для регистрации DataAccess слоя в DI
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<INotesStorage, InMemoryNotesStorage>();
            services.AddSingleton<IUserStorage, InMemoryUserStorage>();

            return services;
        }
    }
}
