using Insane.EntityFramework;
using Insane.Enums;
using Insane.Extensions;
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
    public class IdentityMySqlDbContextFactory : IDesignTimeDbContextFactory<IdentityMySqlDbContext>
    {

        public IdentityMySqlDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<IdentityDbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityDbSettingsConfigurationName}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.MySql;

            DbContextOptionsBuilder<IdentityMySqlDbContext> builder = new DbContextOptionsBuilder<IdentityMySqlDbContext>()
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

            DbContextFlavors<IdentityDbContextBase> flavors = DbContextFlavors<IdentityDbContextBase>.CreateInstance(new Type[] { typeof(IdentityMySqlDbContext) });


            return (IdentityMySqlDbContext)EntityFrameworkExtensions.CreateDbContext(dbContextSettings, flavors, builder);
        }


    }
}
