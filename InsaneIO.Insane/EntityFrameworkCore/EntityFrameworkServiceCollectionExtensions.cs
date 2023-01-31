using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InsaneIO.Insane.Extensions
{
    //public static class EntityFrameworkServiceCollectionExtensions
    //{

    //    public static IServiceCollection AddDbContext<TBaseContext, TSqlServerContext, TPostgreSqlContext, TMySqlContext, TOracleContext, TSettings>(
    //        this IServiceCollection services,
    //        string optionsName,
    //        IConfigurationSection configuration, 
    //        TSettings settings, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder>? builderAction = null, DbContextOptionsBuilderActionFlavors? builderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)

    //        where TBaseContext : CoreDbContextBase
    //        where TSqlServerContext : TBaseContext
    //        where TPostgreSqlContext : TBaseContext
    //        where TMySqlContext : TBaseContext
    //        where TOracleContext : TBaseContext
    //        where TSettings : DbContextSettings, new()
    //    {
    //        services.AddOptions<TSettings>(optionsName).Bind(configuration).ValidateDataAnnotations();
            
    //        services.AddDbContext<TContext, TSettings>(settingsAction, constructorAdditionalParameters, dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors, lifetime);
    //        return services;
    //    }




    //    public static IServiceCollection AddDbContext<TBaseContext, TSqlServerContext, TPostgreSqlContext, TMySqlContext, TOracleContext, TSettings>(this IServiceCollection services, string settingsInstanceName, Action<TSettings> settingsAction, List<object>? constructorAdditionalParameters = null, Action<DbContextOptionsBuilder<TContext>>? dbContextOptionsBuilderAction = null, DbContextOptionsBuilderActionFlavors? dbContextOptionsBuilderActionFlavors = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    //        where TBaseContext : CoreDbContextBase
    //        where TSqlServerContext : TBaseContext
    //        where TPostgreSqlContext : TBaseContext
    //        where TMySqlContext : TBaseContext
    //        where TOracleContext : TBaseContext
    //        where TSettings : DbContextSettings, new()
    //    {
    //        if (typeof(TBaseContext).Equals(typeof(CoreDbContextBase)))
    //        {
    //            throw new ArgumentException($"\"{nameof(TContext)}\" type parameter cannot be \"{nameof(CoreDbContextBase)}\".");
    //        }

    //        constructorAdditionalParameters ??= new List<object>();
    //        services.Configure(settingsInstanceName, settingsAction);
    //        Func<IServiceProvider, TContext> factory = (serviceProvider) =>
    //        {
    //            IOptionsMonitor<TSettings> options = serviceProvider.GetRequiredService<IOptionsMonitor<TSettings>>();
    //            TSettings settings = options.Get(settingsInstanceName);
    //            DbContextOptionsBuilder<TContext> builder = settings.ConfigureDbContextProviderOptions(dbContextOptionsBuilderAction, dbContextOptionsBuilderActionFlavors);
    //            constructorAdditionalParameters.Insert(0, builder);
    //            return (TContext)Activator.CreateInstance(typeof(TContext), constructorAdditionalParameters.ToArray())!;
    //        };

    //        switch (lifetime)
    //        {
    //            case ServiceLifetime.Scoped:
    //                services.AddScoped(factory);
    //                break;
    //            case ServiceLifetime.Transient:
    //                services.AddTransient(factory);
    //                break;
    //            case ServiceLifetime.Singleton:
    //                services.AddSingleton(factory);
    //                break;
    //            default:
    //                throw new NotImplementedException($"{nameof(ServiceLifetime)} {lifetime}");
    //        }
    //        return services;
    //    }



    //}
}

