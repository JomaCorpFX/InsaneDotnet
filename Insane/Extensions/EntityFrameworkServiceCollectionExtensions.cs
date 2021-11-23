using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        public static IServiceCollection AddDbContext<TContextBase>(this IServiceCollection services, DbContextSettings settings, DbContextFlavors<TContextBase> flavors, Action<DbContextOptionsBuilder> dbContextOptionsBuilderAction = null!, DbContextOptionsBuilderActionFlavors dbContextOptionsBuilderActionFlavors = null!, ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TContextBase : CoreDbContextBase
        {

            if (typeof(TContextBase).Equals(typeof(CoreDbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TContextBase)}\" type parameter cannot be \"{nameof(CoreDbContextBase)}\".");
            }

            (Type dbContextType, DbContextOptionsBuilder builder)  = settings.ConfigureDbProvider(flavors, dbContextOptionsBuilderAction,dbContextOptionsBuilderActionFlavors);
            
            Func<IServiceProvider, TContextBase> factory = (serviceProvider) =>
            {
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

