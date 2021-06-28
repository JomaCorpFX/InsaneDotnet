using Insane.EntityFramework;
using Insane.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context.Factory
{
    public class IdentityOracleDbContextFactory : IDesignTimeDbContextFactory<Identity1OracleDbContext>
    {
        public Identity1OracleDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().
                   SetBasePath(Directory.GetCurrentDirectory())
                   .AddUserSecrets<Identity1DbContextBase>()
                   .AddJsonFile(IdentityConstants.DefaultConfigurationFile, false, true)
                   .Build();
            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind($"{IdentityConstants.InsaneIdentityConfigurationName}:{nameof(DbContextSettings)}", dbContextSettings);
            dbContextSettings.Provider = DbProvider.Oracle;

            DbContextOptionsBuilder<Identity1OracleDbContext> builder = new DbContextOptionsBuilder<Identity1OracleDbContext>()
                .UseOracle(dbContextSettings.OracleConnectionString)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);

            Action<OracleDbContextOptionsBuilder> providerBuilder = (options) =>
            {
                Console.WriteLine("OracleDbContextOptionsBuilder executed.");
            };

            builder.UseOracle(providerBuilder);



            DbContextFlavors flavors = DbContextFlavors.CreateInstance<Identity1DbContextBase>(new Type[] { typeof(Identity1OracleDbContext) });

            return (Identity1OracleDbContext)DbContextExtensions.CreateDbContext<Identity1DbContextBase>(dbContextSettings, flavors, builder);
        }
    }
}
