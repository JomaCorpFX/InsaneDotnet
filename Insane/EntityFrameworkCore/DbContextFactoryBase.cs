using Insane.EntityFrameworkCore;
using Insane.Enums;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Oracle.EntityFrameworkCore.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
{
    public abstract class DbContextFactoryBase<TContextBase, TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : TContextBase
        where TContextBase : CoreDbContextBase
    {
        public abstract string ConfigurationFilename { get; }
        public abstract string ConfigurationPath { get; }
        public abstract Type UserSecretsType { get; }
        public abstract DbContextOptionsBuilderActionFlavors DbContextOptionsBuilderActionFlavors { get; }
        public abstract Action<DbContextOptionsBuilder> DbContextOptionsBuilderAction { get; }

        public virtual TContext CreateDbContext(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory());
            if (UserSecretsType != null)
            {
                var addUserSecretsMethod = typeof(UserSecretsConfigurationExtensions).GetMethod(nameof(UserSecretsConfigurationExtensions.AddUserSecrets), new Type[] { typeof(IConfigurationBuilder) })!.MakeGenericMethod(UserSecretsType);
                configurationBuilder = (IConfigurationBuilder)addUserSecretsMethod.Invoke(configurationBuilder, new object[] { configurationBuilder })!;
            }
            configurationBuilder = configurationBuilder.AddJsonFile(ConfigurationFilename, false, true);
            IConfiguration configuration = configurationBuilder.Build();

            DbContextSettings dbContextSettings = new DbContextSettings();
            configuration.Bind(ConfigurationPath, dbContextSettings);

            switch (typeof(TContext).GetInterfaces())
            {
                case var interfaces when interfaces.Contains(typeof(ISqlServerDbContext)):
                    dbContextSettings.Provider = DbProvider.SqlServer;
                    break;
                case var interfaces when interfaces.Contains(typeof(IPostgreSqlDbContext)):
                    dbContextSettings.Provider = DbProvider.PostgreSql;
                    break;
                case var interfaces when interfaces.Contains(typeof(IMySqlDbContext)):
                    dbContextSettings.Provider = DbProvider.MySql;
                    break;
                case var interfaces when interfaces.Contains(typeof(IOracleDbContext)):
                    dbContextSettings.Provider = DbProvider.Oracle;
                    break;
            }
            
            DbContextFlavors<TContextBase> flavors = DbContextFlavors<TContextBase>.CreateInstance(new Type[] { typeof(TContext) });
            return (TContext)dbContextSettings.CreateDbContext(flavors, DbContextOptionsBuilderAction, DbContextOptionsBuilderActionFlavors);
        }

    }
}



//public virtual TContext CreateDbContext(string[] args)
//{
//    Action<TContextOptionsBuilder> contextOptionsBuilder = (ProviderAction == null) ? (options) => { } : ProviderAction;
//    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
//      .SetBasePath(Directory.GetCurrentDirectory());
//    if (UserSecretsType != null)
//    {
//        var addUserSecretsMethod = typeof(UserSecretsConfigurationExtensions).GetMethod(nameof(UserSecretsConfigurationExtensions.AddUserSecrets), new Type[] { typeof(IConfigurationBuilder) })!.MakeGenericMethod(UserSecretsType);
//        configurationBuilder = (IConfigurationBuilder)addUserSecretsMethod.Invoke(configurationBuilder, new object[] { configurationBuilder })!;
//    }
//    configurationBuilder = configurationBuilder.AddJsonFile(ConfigurationFilename, false, true);
//    IConfiguration configuration = configurationBuilder.Build();

//    DbContextSettings dbContextSettings = new DbContextSettings();
//    configuration.Bind(ConfigurationPath, dbContextSettings);
//    DbContext.prov

//            DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>()
//                .EnableSensitiveDataLogging(true)
//                .EnableDetailedErrors(true);

//    switch (typeof(TContext).GetInterfaces())
//    {
//        case var interfaces when interfaces.Contains(typeof(ISqlServerDbContext)):
//            Action<SqlServerDbContextOptionsBuilder> optionsActionSqlServer = (options) =>
//            {
//                options.MigrationsAssembly(MigrationAssembly.FullName);
//            };
//            builder.UseSqlServer(optionsActionSqlServer);
//            builder.UseSqlServer(contextOptionsBuilder as Action<SqlServerDbContextOptionsBuilder>);
//            dbContextSettings.Provider = DbProvider.SqlServer;
//            break;

//        case var interfaces when interfaces.Contains(typeof(IPostgreSqlDbContext)):
//            Action<NpgsqlDbContextOptionsBuilder> optionsActionPostgreSql = (options) =>
//            {
//                options.MigrationsAssembly(MigrationAssembly.FullName);
//            };
//            builder.UseNpgsql(optionsActionPostgreSql);
//            builder.UseNpgsql(contextOptionsBuilder as Action<NpgsqlDbContextOptionsBuilder>);
//            dbContextSettings.Provider = DbProvider.PostgreSql;
//            break;

//        case var interfaces when interfaces.Contains(typeof(IMySqlDbContext)):
//            Action<MySqlDbContextOptionsBuilder> optionsActionMySql = (options) =>
//            {
//                options.MigrationsAssembly(MigrationAssembly.FullName);
//            };
//            builder.UseMySql(ServerVersion.AutoDetect(dbContextSettings.MySqlConnectionString), optionsActionMySql);
//            builder.UseMySql(ServerVersion.AutoDetect(dbContextSettings.MySqlConnectionString), contextOptionsBuilder as Action<MySqlDbContextOptionsBuilder>);
//            dbContextSettings.Provider = DbProvider.MySql;
//            break;

//        case var interfaces when interfaces.Contains(typeof(IOracleDbContext)):
//            Action<OracleDbContextOptionsBuilder> optionsActionOracle = (options) =>
//            {
//                options.MigrationsAssembly(MigrationAssembly.FullName);
//            };
//            builder.UseOracle(optionsActionOracle);
//            builder.UseOracle(contextOptionsBuilder as Action<OracleDbContextOptionsBuilder>);
//            dbContextSettings.Provider = DbProvider.Oracle;
//            break;
//    }
//    DbContextFlavors<TContextBase> flavors = DbContextFlavors<TContextBase>.CreateInstance(new Type[] { typeof(TContext) });
//    return (TContext)EntityFrameworkExtensions.CreateDbContext(dbContextSettings, flavors, builder);
//}