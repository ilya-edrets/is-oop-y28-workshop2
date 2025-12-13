using AspNetCore.Authentication.Basic;
using Core.Extensions;
using DataAccess.Postgres.Extensions;
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

            DatabaseExtension.MigrateDatabase(builder.Configuration.GetConnectionString("postgres"));

            app.Run();
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
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
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
