using Insane.EntityFramework;
using Insane.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context.Factory
{
    public class IdentityMySqlDbContextFactory : IDesignTimeDbContextFactory<Identity1MySqlDbContext>
    {
        
        public Identity1MySqlDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<Identity1DbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityConfigurationName}:{nameof(DbContextSettings)}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.MySql;

            DbContextOptionsBuilder<Identity1MySqlDbContext> builder = new DbContextOptionsBuilder<Identity1MySqlDbContext>()
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<MySqlDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("MySqlDbContextOptionsBuilder 1 executed.");
            };

            builder.UseMySql(ServerVersion.AutoDetect(dbContextSettings.MySqlConnectionString), providerBuilder);

            providerBuilder = (options) =>
            {
                Console.WriteLine("MySqlDbContextOptionsBuilder 2 executed.");
            };

            builder.UseMySql(ServerVersion.AutoDetect(dbContextSettings.MySqlConnectionString), providerBuilder);

            DbContextFlavors flavors = DbContextFlavors.CreateInstance<Identity1DbContextBase>(new Type[] { typeof(Identity1MySqlDbContext) });


            return (Identity1MySqlDbContext)DbContextExtensions.CreateDbContext<Identity1DbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
