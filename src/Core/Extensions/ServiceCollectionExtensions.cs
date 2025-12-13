using Core.Abstractions;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddLogging();

            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<INotesService, NotesService>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddSingleton<IUserService, UserService>();

            return services;
        }
    }
}
