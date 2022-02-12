using Insane.Core;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.IO;
using System.Linq;

namespace Insane.EntityFrameworkCore
{
    public abstract class DbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : CoreDbContextBase<TContext>
    {

        public class ConfigureSettingsParameters
        {
            public string? ConfigurationFilename { get; set; } = null;
            public string[]? CommandLineArgs { get; set; } = null;
            public string? ConfigurationPath { get; set; } = null;
            public ICollection<Type>? SecretTypes { get; set; } = null;
        }

        public virtual Action<DbContextSettings, ConfigureSettingsParameters> SettingsConfigureAction { get; } = (dbContextSettings, parameters) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile(parameters.ConfigurationFilename ?? EntityFrameworkCoreConstants.DefaultConfigurationFilename, false, true);
            if (parameters.SecretTypes is not null && parameters.SecretTypes.Count > 0)
            {
                foreach (var secretType in parameters.SecretTypes)
                {
                    var addUserSecretsMethod = typeof(UserSecretsConfigurationExtensions).GetMethod(nameof(UserSecretsConfigurationExtensions.AddUserSecrets), new Type[] { typeof(IConfigurationBuilder) })!.MakeGenericMethod(secretType);
                    configurationBuilder = (IConfigurationBuilder)addUserSecretsMethod.Invoke(configurationBuilder, new object[] { configurationBuilder })!;
                }
            }
            if (parameters.CommandLineArgs is not null && parameters.CommandLineArgs.Length > 0)
            {
                configurationBuilder.AddCommandLine(parameters.CommandLineArgs);
            }
            IConfiguration configuration = configurationBuilder.Build();
            configuration.Bind(parameters.ConfigurationPath ?? EntityFrameworkCoreConstants.DefaultConfigurationPath, dbContextSettings);
        };

        public virtual DbContextOptionsBuilderActionFlavors DbContextOptionsBuilderActionFlavors { get; } = new DbContextOptionsBuilderActionFlavors()
        {
            SqlServer = (builder) =>
            {

            },

            PostgreSql = (builder) =>
            {

            },

            MySql = (builder) =>
            {

            },

            Oracle = (builder) =>
            {

            }
        };

        public virtual Action<DbContextOptionsBuilder<TContext>> DbContextOptionsBuilderAction { get; } = (builder) =>
        {
            builder.EnableDetailedErrors();
            builder.EnableSensitiveDataLogging();
        };

        public virtual List<object?> ConstructorAdditionalParameters { get; } = new List<object?> { };

        public virtual TContext CreateDbContext(string[] args)
        {
            ConfigureSettingsParameters parameters = new ConfigureSettingsParameters()
            {
                SecretTypes = new List<Type>()
            };

            string? secretTypeNames = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.SecretTypes)}=")).Select(e => e.Split("=")[1]).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(secretTypeNames))
            {
                foreach (var typename in secretTypeNames.Split(","))
                {
                    parameters.SecretTypes.Add(Type.GetType(typename)!);
                }
            }
            parameters.ConfigurationFilename = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationFilename)}=")).FirstOrDefault()?.Split(",")[1];
            parameters.ConfigurationPath = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationPath)}=")).FirstOrDefault()?.Split(",")[1];
            parameters.CommandLineArgs = args;

            DbContextSettings dbContextSettings = new DbContextSettings();
            SettingsConfigureAction.Invoke(dbContextSettings, parameters);

            DbContextOptionsBuilder<TContext> builder = dbContextSettings.ConfigureDbProvider(DbContextOptionsBuilderAction, DbContextOptionsBuilderActionFlavors);
            ConstructorAdditionalParameters.Insert(0, builder.Options);
            return (TContext)Activator.CreateInstance(typeof(TContext), ConstructorAdditionalParameters.ToArray())!;
        }
    }
}
