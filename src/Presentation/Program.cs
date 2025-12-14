using AspNetCore.Authentication.Basic;
using Core.Extensions;
using DataAccess.Postgres.Extensions;
using DataAccess.Postgres.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Presentation.Authentication;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Configuration, builder.Services);

            var app = builder.Build();
            Configure(app);

            DatabaseHelper.MigrateDatabase(builder.Configuration.GetConnectionString("postgres"));

            app.Run();
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // IServiceCollection реализует паттерн Билдер для DI-контейнера,
            // В тестах можно увидеть весь процесс создания
            services.AddLogging();
            services.AddControllers();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                opt.AddSecurityDefinition(BasicDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                });
            });
            services.AddAuthentication(BasicDefaults.AuthenticationScheme)
                .AddBasic<BasicAuthenticationService>(opt => { opt.Realm = "My App"; });

            // Добавляем свои слои в DI
            services.AddCore();
            services.AddDataAccess(configuration.GetConnectionString("postgres"));
        }

        private static void Configure(WebApplication app)
        {
            // Миддлвары реализуют паттерн Цепочка Обязанностей для обработки входящего запроса
            // Порядок добавления миддлвар важен.
            // Например, сваггер добавлен до авторизации и аутентификации
            // Это позволяет использовать сваггер незалогиненным пользователям

            app.UseSwagger(); // добавлет эндпоинт swagger/v1/swagger.json где будет описана спецификация нашего API
            app.UseSwaggerUI(); // Добавляет UI по эндпоинту swagger/index.html, который генерируется на основе swagger.json
            app.UseAuthentication(); // Добавляет поддержку аутентификации. Аутентификация заполняет объект base.User.Identity доступный в контроллерах
            app.UseAuthorization(); // Добавляет проверку прав доступа при обращении к эндпоинтам
            app.MapControllers(); // вызывает нужный контроллер в зависимости от запрашиваего урла
        }
    }
}
