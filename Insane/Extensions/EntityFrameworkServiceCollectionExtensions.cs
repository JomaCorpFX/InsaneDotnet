using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class EntityFrameworkServiceCollectionExtensions
    {

        public static IServiceCollection AddDbContext<TContextBase, TSettings>(this IServiceCollection services, TSettings settings, DbContextFlavors<TContextBase> flavors, Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction = null!, DbContextOptionsBuilderActionFlavors dbContextOptionsBuilderActionFlavors = null!, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContextBase : CoreDbContextBase
            where TSettings : DbContextSettings, new()
        {
            Action<TSettings> settingsAction = (s) =>
            {
                s.Provider = settings.Provider;
                s.SqlServerConnectionString = settings.SqlServerConnectionString;
                s.PostgreSqlConnectionString = settings.PostgreSqlConnectionString;
                s.MySqlConnectionString = settings.MySqlConnectionString;
                s.OracleConnectionString = settings.OracleConnectionString;
            };

            services.AddDbContext(settingsAction, flavors, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }

        public static IServiceCollection AddDbContext<TContextBase, TSettings>(this IServiceCollection services, IConfigurationSection settingsSection, DbContextFlavors<TContextBase> flavors, Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction = null!, DbContextOptionsBuilderActionFlavors dbContextOptionsBuilderActionFlavors = null!, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContextBase : CoreDbContextBase
            where TSettings : DbContextSettings, new()
        {
            Action<TSettings> settingsAction = (s) =>
            {
                TSettings settings = settingsSection.Get<TSettings>();
                s.Provider = settings.Provider;
                s.SqlServerConnectionString = settings.SqlServerConnectionString;
                s.PostgreSqlConnectionString = settings.PostgreSqlConnectionString;
                s.MySqlConnectionString = settings.MySqlConnectionString;
                s.OracleConnectionString = settings.OracleConnectionString;
            };
            services.AddDbContext(settingsAction, flavors, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }


        public static IServiceCollection AddDbContext<TContextBase, TSettings>(this IServiceCollection services, Action<TSettings> settingsAction, DbContextFlavors<TContextBase> flavors, Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction = null!, DbContextOptionsBuilderActionFlavors dbContextOptionsBuilderActionFlavors = null!, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContextBase : CoreDbContextBase
            where TSettings : DbContextSettings, new()
        {
            services.AddDbContext("", settingsAction, flavors, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }

        public static IServiceCollection AddDbContext<TContextBase, TSettings>(this IServiceCollection services, string settingsInstanceName, Action<TSettings> settingsAction, DbContextFlavors<TContextBase> flavors, Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction = null!, DbContextOptionsBuilderActionFlavors dbContextOptionsBuilderActionFlavors = null!, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContextBase : CoreDbContextBase
            where TSettings : DbContextSettings, new()
        {
            if (typeof(TContextBase).Equals(typeof(CoreDbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TContextBase)}\" type parameter cannot be \"{nameof(CoreDbContextBase)}\".");
            }

            services.Configure(settingsInstanceName, settingsAction);
            Func<IServiceProvider, TContextBase> factory = (serviceProvider) =>
            {
                IOptionsMonitor<TSettings> options = serviceProvider.GetRequiredService<IOptionsMonitor<TSettings>>();
                TSettings settings = options.Get(settingsInstanceName);
                (Type dbContextType, DbContextOptionsBuilder builder) = settings.ConfigureDbProvider(flavors, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors);
                return dbContextType.CreateDbContext<TContextBase>(builder);
            };

            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(factory);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(factory);
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton(factory);
                    break;
                default:
                    throw new NotImplementedException($"{nameof(ServiceLifetime)} {lifetime}");
            }
            return services;
        }

        

    }
}

