using Insane.EntityFramework;
using Insane.Enums;
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
    public class IdentityPostgreSqlDbContextFactory : IDesignTimeDbContextFactory<Identity1PostgreSqlDbContext>
    {
        public Identity1PostgreSqlDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<Identity1DbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityConfigurationName}:{nameof(DbContextSettings)}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.PostgreSql;

            DbContextOptionsBuilder<Identity1PostgreSqlDbContext> builder = new DbContextOptionsBuilder<Identity1PostgreSqlDbContext>()
                .UseNpgsql(dbContextSettings.PostgreSqlConnectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<NpgsqlDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("NpgsqlDbContextOptionsBuilder executed.");
            };

            builder.UseNpgsql(providerBuilder);

            DbContextFlavors flavors = DbContextFlavors.CreateInstance<Identity1DbContextBase>(new Type[] { typeof(Identity1PostgreSqlDbContext) });

            return (Identity1PostgreSqlDbContext)DbContextExtensions.CreateDbContext<Identity1DbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
