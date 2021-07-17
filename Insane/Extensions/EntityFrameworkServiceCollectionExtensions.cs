using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private static Type GetDbContextType<TCoreDbContext>(this DbContextOptionsBuilder builder, IConfiguration configuration, string configPath, DbContextFlavors<TCoreDbContext> flavors)
            where TCoreDbContext:CoreDbContextBase
        {
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind(configPath, dbContextSettings);
            Type dbContextType = builder.ConfigureDbProvider(dbContextSettings, flavors);
            return dbContextType;
        }

        public static IServiceCollection AddDbContext<TCoreDbContext>(this IServiceCollection services, DbContextOptionsBuilder builder, IConfiguration configuration, string configurationPath, DbContextFlavors<TCoreDbContext> flavors, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TCoreDbContext : CoreDbContextBase
        {
            if (typeof(TCoreDbContext).Equals(typeof(CoreDbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TCoreDbContext)}\" type parameter cannot be \"{nameof(CoreDbContextBase)}\".");
            }

            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped<TCoreDbContext>((serviceProvider) =>
                    { 
                        Type dbContextType = GetDbContextType(builder, configuration, configurationPath, flavors);
                        var exNew = Expression.New(dbContextType.GetConstructor(new Type[] { typeof(DbContextOptions) })!, Expression.Constant(builder.Options));
                        var exConvert = Expression.Convert(exNew, typeof(TCoreDbContext));
                        return Expression.Lambda<Func<TCoreDbContext>>(exConvert).Compile().Invoke();
                    });
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TCoreDbContext>((serviceProvider) =>
                    {
                        Type dbContextType = GetDbContextType(builder, configuration, configurationPath, flavors);
                        var exNew = Expression.New(dbContextType.GetConstructor(new Type[] { typeof(DbContextOptions) })!, Expression.Constant(builder.Options));
                        var exConvert = Expression.Convert(exNew, typeof(TCoreDbContext));
                        return Expression.Lambda<Func<TCoreDbContext>>(exConvert).Compile().Invoke();
                    });
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TCoreDbContext>((serviceProvider) =>
                    {
                        Type dbContextType = GetDbContextType(builder, configuration, configurationPath, flavors);
                        var exNew = Expression.New(dbContextType.GetConstructor(new Type[] { typeof(DbContextOptions) })!, Expression.Constant(builder.Options));
                        var exConvert = Expression.Convert(exNew, typeof(TCoreDbContext));
                        return Expression.Lambda<Func<TCoreDbContext>>(exConvert).Compile().Invoke();
                    });
                    break;
                default:
                    throw new NotImplementedException("Not implemented lifetime.");
            }
            return services;
        }

    }
}



//public static IServiceCollection AddDbContext<TCoreDbContext>(this IServiceCollection services, DbContextOptionsBuilder builder, IConfiguration configuration, string configurationPath, DbContextFlavors flavors, ServiceLifetime lifetime = ServiceLifetime.Scoped)
//            where TCoreDbContext : DbContextBase
//{
//    if (typeof(TCoreDbContext).Equals(typeof(DbContextBase)))
//    {
//        throw new ArgumentException($"\"{nameof(TCoreDbContext)}\" type parameter cannot be \"{nameof(DbContextBase)}\".");
//    }


//    string methodName = null!;
//    switch (lifetime)
//    {
//        case ServiceLifetime.Scoped:
//            methodName = nameof(ServiceCollectionServiceExtensions.AddScoped);
//            break;
//        case ServiceLifetime.Transient:
//            methodName = nameof(ServiceCollectionServiceExtensions.AddTransient);
//            break;
//        case ServiceLifetime.Singleton:
//            methodName = nameof(ServiceCollectionServiceExtensions.AddSingleton);
//            break;
//        default:
//            throw new NotImplementedException("Not implemented lifetime.");
//    }

//    var methodInfo = typeof(ServiceCollectionServiceExtensions).GetMethods()
//                                .Where(m => m.Name.Equals(methodName))
//                                .Select(m => new
//                                {
//                                    Method = m,
//                                    Args = m.GetGenericArguments(),
//                                    Params = m.GetParameters()
//                                })
//                                .Where(x => x.Args.Length == 2 && x.Params.Length == 2 && "TService".Equals(x.Args[0].Name) && "TImplementation".Equals(x.Args[1].Name))
//                                .First();


//    DbContextSettings dbContextSettings = new DbContextSettings();
//    configuration.Bind(configurationPath, dbContextSettings);
//    Type dbContextType = builder.ConfigureDbProvider(dbContextSettings, flavors);

//    var exCallGetDbContextType = Expression.Call(typeof(EntityFrameworkServiceCollectionExtensions).GetMethod(nameof(GetDbContextType))!,
//         Expression.Constant(builder),
//         Expression.Constant(configuration),
//         Expression.Constant(configurationPath),
//         Expression.Constant(flavors));

//    var exLambdaGetDbContextType = Expression.Lambda<Func<Type>>(exCallGetDbContextType);

//    var genericMethod = methodInfo.Method.MakeGenericMethod(new Type[] { typeof(TCoreDbContext), dbContextType });
//    var exParameter = Expression.Parameter(typeof(IServiceProvider));
//    var exNew = Expression.New(dbContextType.GetConstructor(new Type[] { typeof(DbContextOptions) })!, Expression.Constant(builder.Options));
//    var exConvert = Expression.Convert(exNew, dbContextType);
//    var exLambda = Expression.Lambda(exConvert, exParameter);


//    genericMethod.Invoke(null, new object[] { services, exLambda.Compile() });
//    return services;
//}