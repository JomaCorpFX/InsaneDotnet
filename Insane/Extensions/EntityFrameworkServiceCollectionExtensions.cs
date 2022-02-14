using Insane.Core;
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

        public static IServiceCollection AddDbContext<TContext, TSettings>(this IServiceCollection services, TSettings settings, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder>? dbContextOptionsBuilderAction = null, DbContextOptionsBuilderActionFlavors? dbContextOptionsBuilderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContext : CoreDbContextBase<TContext>, ISelfReference<TContext>
            where TSettings : DbContextSettings, new()
        {
            Action<TSettings> settingsAction = (s) =>
            {
                //TODO: ARREGLAR PROVIDER s.Provider = settings.Provider;
                s.SqlServerConnectionString = settings.SqlServerConnectionString;
                s.PostgreSqlConnectionString = settings.PostgreSqlConnectionString;
                s.MySqlConnectionString = settings.MySqlConnectionString;
                s.OracleConnectionString = settings.OracleConnectionString;
            };

            services.AddDbContext<TContext, TSettings>(settingsAction, constructorAdditionalParameters, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }

        public static IServiceCollection AddDbContext<TContext, TSettings>(this IServiceCollection services, IConfigurationSection settingsSection, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder>? dbContextOptionsBuilderAction = null, DbContextOptionsBuilderActionFlavors? dbContextOptionsBuilderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContext : CoreDbContextBase<TContext>, ISelfReference<TContext>
            where TSettings : DbContextSettings, new()
        {
            Action<TSettings> settingsAction = (s) =>
            {
                TSettings settings = settingsSection.Get<TSettings>();
                //TODO: ARREGLAR PROVIDER s.Provider = settings.Provider;
                s.SqlServerConnectionString = settings.SqlServerConnectionString;
                s.PostgreSqlConnectionString = settings.PostgreSqlConnectionString;
                s.MySqlConnectionString = settings.MySqlConnectionString;
                s.OracleConnectionString = settings.OracleConnectionString;
            };
            services.AddDbContext<TContext, TSettings>(settingsAction, constructorAdditionalParameters, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }


        public static IServiceCollection AddDbContext<TContext, TSettings>(this IServiceCollection services, Action<TSettings> settingsAction, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder>? dbContextOptionsBuilderAction = null, DbContextOptionsBuilderActionFlavors? dbContextOptionsBuilderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContext : CoreDbContextBase<TContext>, ISelfReference<TContext>
            where TSettings : DbContextSettings, new()
        {
            services.AddDbContext<TContext, TSettings>("", settingsAction, constructorAdditionalParameters, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
            return services;
        }

        public static IServiceCollection AddDbContext<TContext, TSettings>(this IServiceCollection services, string settingsInstanceName, Action<TSettings> settingsAction, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder<TContext>>? dbContextOptionsBuilderAction = null, DbContextOptionsBuilderActionFlavors? dbContextOptionsBuilderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContext : CoreDbContextBase<TContext>, ISelfReference<TContext>
            where TSettings : DbContextSettings, new()
        {
            if (typeof(TContext).Equals(typeof(CoreDbContextBase<TContext>)))
            {
                throw new ArgumentException($"\"{nameof(TContext)}\" type parameter cannot be \"{nameof(CoreDbContextBase<TContext>)}\".");
            }

            constructorAdditionalParameters ??= new List<object>();
            services.Configure(settingsInstanceName, settingsAction);
            Func<IServiceProvider, TContext> factory = (serviceProvider) =>
            {
                IOptionsMonitor<TSettings> options = serviceProvider.GetRequiredService<IOptionsMonitor<TSettings>>();
                TSettings settings = options.Get(settingsInstanceName);
                DbContextOptionsBuilder<TContext> builder = settings.ConfigureDbContextProviderOptions(dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors);
                constructorAdditionalParameters.Insert(0, builder);
                return (TContext)Activator.CreateInstance(typeof(TContext), constructorAdditionalParameters.ToArray())!;
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

