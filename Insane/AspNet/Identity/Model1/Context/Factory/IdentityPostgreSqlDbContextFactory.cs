using Insane.EntityFramework;
using Insane.Enums;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context.Factory
{
    public class IdentityPostgreSqlDbContextFactory : IDesignTimeDbContextFactory<IdentityPostgreSqlDbContext>
    {
        public IdentityPostgreSqlDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<IdentityDbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityDbSettingsConfigurationName}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.PostgreSql;

            DbContextOptionsBuilder<IdentityPostgreSqlDbContext> builder = new DbContextOptionsBuilder<IdentityPostgreSqlDbContext>()
                .UseNpgsql(dbContextSettings.PostgreSqlConnectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<NpgsqlDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("NpgsqlDbContextOptionsBuilder executed.");
            };

            builder.UseNpgsql(providerBuilder);

            DbContextFlavors<IdentityDbContextBase> flavors = DbContextFlavors<IdentityDbContextBase>.CreateInstance(new Type[] { typeof(IdentityPostgreSqlDbContext) });

            return (IdentityPostgreSqlDbContext)EntityFrameworkExtensions.CreateDbContext<IdentityDbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
