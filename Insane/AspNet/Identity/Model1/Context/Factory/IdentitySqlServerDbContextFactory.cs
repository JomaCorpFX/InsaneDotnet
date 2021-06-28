using Insane.EntityFramework;
using Insane.Enums;
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
    class IdentitySqlServerDbContextFactory : IDesignTimeDbContextFactory<Identity1SqlServerDbContext>
    {
        public Identity1SqlServerDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<Identity1DbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityConfigurationName}:{nameof(DbContextSettings)}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.SqlServer;

            DbContextOptionsBuilder<Identity1SqlServerDbContext> builder = new DbContextOptionsBuilder<Identity1SqlServerDbContext>()
                .UseSqlServer(dbContextSettings.SqlServerConnectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<SqlServerDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("SqlServerDbContextOptionsBuilder executed.");
            };
            builder.UseSqlServer(providerBuilder);

            DbContextFlavors flavors = DbContextFlavors.CreateInstance<Identity1DbContextBase>(new Type[] { typeof(Identity1SqlServerDbContext) });

            return (Identity1SqlServerDbContext)DbContextExtensions.CreateDbContext<Identity1DbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
