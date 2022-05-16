using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Eventify.DAL.Infrastructure
{
    public static class DatabaseConfiguration
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            var typeOfDatabase = configuration["Database"];
            switch (typeOfDatabase)
            {
                case "PostgreSQL":
                    RegisterPostgreSql(services, configuration);
                    break;
                default:
                    RegisterPostgreSql(services, configuration);
                    break;
            }
        }

        /*public static IdentityBuilder RegisterIdentityContext(this IdentityBuilder identityBuilder)
        {
            return identityBuilder.AddEntityFrameworkStores<AppDbContext>();
        }*/

        private static void RegisterPostgreSql(IServiceCollection services, IConfiguration configuration)
        {
            var systemConnectionString = configuration["ConnectionStrings:MainConnection"];
            var password = configuration["PostgreSql:Password"];
            var systemContextBuilder = new NpgsqlConnectionStringBuilder(systemConnectionString)
            {
                Password = password
            };
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(systemContextBuilder.ConnectionString, b => b.MigrationsAssembly("Eventify.DAL")), ServiceLifetime.Scoped, ServiceLifetime.Singleton
            );
        }
    }
}
