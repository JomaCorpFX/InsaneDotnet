using InsaneIO.Insane.EntityFrameworkCore.MySql.Metadata.Internal;
using InsaneIO.Insane.EntityFrameworkCore.MySql.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Oracle.EntityFrameworkCore.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System.Collections.Immutable;

namespace InsaneIO.Insane.EntityFrameworkCore
{

    public abstract class CoreDbContextBase : DbContext
    {

#pragma warning disable EF1001 // Internal EF Core API usage.
        public static readonly Type SqlServerOptionsExtensionType = typeof(SqlServerOptionsExtension);
        public static readonly Type PostgreSqlOptionsExtensionType = typeof(NpgsqlOptionsExtension);
        public static readonly Type MySqlOptionsExtensionType = typeof(MySqlOptionsExtension);
        public static readonly Type OracleOptionsExtensionType = typeof(OracleOptionsExtension);
#pragma warning restore EF1001 // Internal EF Core API usage.


        public static readonly ImmutableDictionary<Type, Type> DbProviderTypes = new Dictionary<Type, Type>()
        {
            { typeof(ISqlServerDbContext) , SqlServerOptionsExtensionType },
            { typeof(IPostgreSqlDbContext),PostgreSqlOptionsExtensionType },
            { typeof(IMySqlDbContext), MySqlOptionsExtensionType },
            { typeof(IOracleDbContext),OracleOptionsExtensionType }
        }.ToImmutableDictionary();

        private readonly Type ImplementedDbProviderInterface = null!;

        static CoreDbContextBase()
        {
            
        }

        public static Type? GetImplementedDbProviderInterface(Type type, bool NoExceptions)
        {
            IEnumerable<Type> implementedInterfaces = type.GetInterfaces().Where(x => DbProviderTypes.Keys.Contains(x));
            if (implementedInterfaces.Any())
            {
                if (implementedInterfaces.Count() > 1)
                {
                    return NoExceptions? null : throw new InvalidOperationException($"Multiple db provider interface are implemented for this DbContext. Found: {string.Join(", ", implementedInterfaces.Select(e => e.Name))}. Implement only one of the following: {string.Join(", ", DbProviderTypes.Keys.Select(e => e.Name))}");
                }
            }
            else
            {
                return NoExceptions? null: throw new InvalidOperationException($"No db provider interface is implemented for this DbContext ({type.FullName}). Implement only one of the following: {string.Join(", ", DbProviderTypes.Keys.Select(e => e.Name))}");
            }
            return implementedInterfaces.FirstOrDefault();

        }

        private CoreDbContextBase()
        {

        }

        public CoreDbContextBase(DbContextOptions options) : base(options)
        {
            ImplementedDbProviderInterface = GetImplementedDbProviderInterface(GetType(), false)!;
            Type? extension = options.Extensions.Where(x => DbProviderTypes.Values.Contains(x.GetType())).Select(x => x.GetType()).FirstOrDefault();
            if (!DbProviderTypes.TryGetValue(ImplementedDbProviderInterface, out Type? value) || !(value?.Equals(extension) ?? false))
            {
                throw new InvalidOperationException($"Db provider interface implementation({ImplementedDbProviderInterface.Name}) and db provider extension({extension?.Name ?? "No db provider extension configured"}) are incompatible for this DbContext. Them need to be from the same db provider.");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.Options.Extensions.Where(e => e.GetType().Equals(MySqlOptionsExtensionType)).Any())
            {
                ServiceProvider serviceProvider = new ServiceCollection()
                .AddEntityFrameworkMySql()
                .AddSingleton<IRelationalAnnotationProvider, CustomMySqlAnnotationProvider>()
                .AddScoped<IMigrationsSqlGenerator, CustomMySqlMigrationsSqlGenerator>()
                .BuildServiceProvider();
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
