using Insane.EntityFramework;
using Insane.Enums;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context.Factory
{
    class IdentitySqlServerDbContextFactory : IDesignTimeDbContextFactory<IdentitySqlServerDbContext>
    {
        public IdentitySqlServerDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<IdentityDbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityDbSettingsConfigurationName}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.SqlServer;

            DbContextOptionsBuilder<IdentitySqlServerDbContext> builder = new DbContextOptionsBuilder<IdentitySqlServerDbContext>()
                .UseSqlServer(dbContextSettings.SqlServerConnectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<SqlServerDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("SqlServerDbContextOptionsBuilder executed.");
            };

            builder.UseSqlServer(providerBuilder);

            DbContextFlavors<IdentityDbContextBase> flavors = DbContextFlavors<IdentityDbContextBase>.CreateInstance(new Type[] { typeof(IdentitySqlServerDbContext) });

            return (IdentitySqlServerDbContext)EntityFrameworkExtensions.CreateDbContext<IdentityDbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
