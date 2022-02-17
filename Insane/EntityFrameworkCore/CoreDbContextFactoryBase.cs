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
    public abstract class CoreDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : CoreDbContextBase<TContext>
    {

        public class ConfigureSettingsParameters
        {
            public string ConfigurationFilename { get; set; } = null!;
            public List<string> CommandLineArgs { get; set; } = null!;
            public string ConfigurationPath { get; set; } = null!;
            public List<Type> SecretTypes { get; set; } = null!;
        }

        private Action<DbContextSettings, ConfigureSettingsParameters> SettingsConfigureAction { get; set; } = (dbContextSettings, parameters) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile(parameters.ConfigurationFilename, false, true);
            foreach (var secretType in parameters.SecretTypes)
            {
                var addUserSecretsMethod = typeof(UserSecretsConfigurationExtensions).GetMethod(nameof(UserSecretsConfigurationExtensions.AddUserSecrets), new Type[] { typeof(IConfigurationBuilder) })!.MakeGenericMethod(secretType);
                configurationBuilder = (IConfigurationBuilder)addUserSecretsMethod.Invoke(configurationBuilder, new object[] { configurationBuilder })!;
            }
            configurationBuilder.AddCommandLine(parameters.CommandLineArgs.ToArray());
            IConfiguration configuration = configurationBuilder.Build();
            configuration.Bind(parameters.ConfigurationPath, dbContextSettings);
        };

        public virtual DbContextOptionsBuilderActionFlavors DbContextOptionsBuilderActionFlavors { get; set; } = new DbContextOptionsBuilderActionFlavors()
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

        public virtual Action<DbContextOptionsBuilder<TContext>> DbContextOptionsBuilderAction { get; set; } = (builder) =>
        {
            builder.EnableDetailedErrors();
            builder.EnableSensitiveDataLogging();
        };

        public virtual List<object?> ConstructorAdditionalParameters { get; set; } = new List<object?> { };

        public virtual TContext CreateDbContext(string[] args)
        {
            ConfigureSettingsParameters parameters = new ConfigureSettingsParameters()
            {
                SecretTypes = new List<Type> { }
            };

            var secretTypeNames = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.SecretTypes)}=")).Select(e => e.Replace($"{nameof(ConfigureSettingsParameters.SecretTypes)}=", "").Trim('\"'));
            foreach (var typename in secretTypeNames)
            {
                parameters.SecretTypes.Add(Type.GetType(typename)!);
            }

            parameters.ConfigurationFilename = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationFilename)}=")).Select(e => e.Replace($"{nameof(ConfigureSettingsParameters.ConfigurationFilename)}=", "").Trim('\"')).FirstOrDefault() ?? EntityFrameworkCoreConstants.DefaultConfigurationFilename;
            parameters.ConfigurationPath = args.Where(a => a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationPath)}=")).Select(e => e.Replace($"{nameof(ConfigureSettingsParameters.ConfigurationPath)}=", "").Trim('\"')).FirstOrDefault() ?? EntityFrameworkCoreConstants.DefaultConfigurationPath;
            parameters.CommandLineArgs = args.Where(a => !a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationFilename)}=")
                                                      && !a.StartsWith($"{nameof(ConfigureSettingsParameters.ConfigurationPath)}=")
                                                      && !a.StartsWith($"{nameof(ConfigureSettingsParameters.SecretTypes)}=")).ToList();

            DbContextSettings dbContextSettings = new DbContextSettings();
            SettingsConfigureAction.Invoke(dbContextSettings, parameters);

            DbContextOptionsBuilder<TContext> builder = dbContextSettings.ConfigureDbContextProviderOptions(DbContextOptionsBuilderAction, DbContextOptionsBuilderActionFlavors);
            ConstructorAdditionalParameters.Insert(0, builder.Options);
            return (TContext)Activator.CreateInstance(typeof(TContext), ConstructorAdditionalParameters.ToArray())!;
        }
    }
}
