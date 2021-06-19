using Insane.Enums;
using Insane.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public static class DbContextExtensions
    {
        public const int PostgreSqlIdentifierMaxLength = 63;
        public const int MySqlIdentifierMaxLength = 63;
        public const int SqlServerIdentifierMaxLength = 128;
        public const int OracleIdentifierMaxLength = 128;


        public const string PrimaryKeyConstraintPrefix = "P";
        public const string ForeignKeyConstraintPrefix = "F";
        public const string UniqueConstraintPrefix = "U";
        public const string IndexPrefix = "I";

        public const String DefaultSchema = "Default";

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string schema, Action<TableBuilder<TEntity>> buildAction) where TEntity : class
        {
            return builder.ToTable(database, null!, schema, buildAction);
        }

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string schema) where TEntity : class
        {
            return builder.ToTable(database, null!, schema, null!);
        }

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string name, string schema) where TEntity : class
        {
            return builder.ToTable(database, name, schema, null!);
        }

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string name, string schema, Action<TableBuilder<TEntity>> buildAction) where TEntity : class
        {
            buildAction = buildAction is null ? (builder) => { } : buildAction;
            schema = string.IsNullOrWhiteSpace(schema) ? DefaultSchema : schema;
            name = string.IsNullOrWhiteSpace(name) ? typeof(TEntity).Name : name;
            switch (database)
            {
                case DatabaseFacade db when db.IsMySql():
                    builder.ToTable($"{schema}.{name}", buildAction);
                    break;

                case DatabaseFacade db when db.IsSqlServer():
                    builder.ToTable(name, schema, buildAction);
                    break;
                case DatabaseFacade db when db.IsNpgsql():
                    builder.ToTable(name, schema, buildAction);
                    break;
                case DatabaseFacade db when db.IsOracle():
                    builder.ToTable(name, schema, buildAction);
                    break;
                default:
                    throw new NotImplementedException("Unknown database provider.");
            }
            return builder;
        }


        private static string GetConstraintFieldSegments<TEntity>(Expression<Func<TEntity, object>> property) where TEntity : class
        {
            StringBuilder ret = new StringBuilder();
            property.GetExpressionReturnMembersNames().ForEach(value =>
            {
                ret.Append($"_{value}");
            });
            return ret.ToString();
        }

        private static string GetIdentifierName(string name, DatabaseFacade database)
        {
            var maxLength = database switch
            {
                DatabaseFacade db when db.IsSqlServer() => SqlServerIdentifierMaxLength,
                DatabaseFacade db when db.IsNpgsql() => PostgreSqlIdentifierMaxLength,
                DatabaseFacade db when db.IsMySql() => MySqlIdentifierMaxLength,
                DatabaseFacade db when db.IsOracle() => OracleIdentifierMaxLength,
                _ => throw new NotImplementedException("Unknown database provider")
            };
            return name.Length < maxLength ? name : name.Substring(0, maxLength);
        }

        private static string GetPrincipalName(this EntityTypeBuilder builder, DatabaseFacade database)
        {
            string schema = string.IsNullOrWhiteSpace(builder.Metadata.GetSchema()) ? string.Empty : $"_{ builder.Metadata.GetSchema()}";
            switch (database)
            {
                case DatabaseFacade db when db.IsMySql():
                    return $"{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsSqlServer():
                    return $"{schema}_{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsNpgsql():
                    return $"{schema}_{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsOracle():
                    return $"{schema}_{builder.Metadata.GetTableName()}";
                default:
                    throw new NotImplementedException("Unknown database provider.");
            }
        }

        private static string GetConstraintName<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string prefix, Expression<Func<TEntity, object>> expression) where TEntity : class
        {
            return GetIdentifierName($"{prefix}_{GetPrincipalName(builder, database)}_{GetConstraintFieldSegments(expression)}", database);
        }

        private static string GetConstraintName<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, EntityTypeBuilder entityBuilder , DatabaseFacade database, string prefix, Expression<Func<TPrincipal, object>> expression) where TRelated : class where TPrincipal : class
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}_{GetConstraintFieldSegments(expression)}", database);
        }

        private static string GetConstraintName<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, EntityTypeBuilder entityBuilder, DatabaseFacade database, string prefix, Expression<Func<TPrincipal, object>> expression) where TPrincipal : class where TRelated : class
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}_{GetConstraintFieldSegments(expression)}", database);
        }


        public static ReferenceCollectionBuilder<TRelated, TPrincipal> HasForeignKey<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, EntityTypeBuilder entityBuilder, DatabaseFacade database, Expression<Func<TPrincipal, object>> foreignKeyExpression) where TRelated : class where TPrincipal : class
        {
            string name = builder.GetConstraintName(entityBuilder, database, ForeignKeyConstraintPrefix, foreignKeyExpression);
            return builder.HasForeignKey(foreignKeyExpression).HasConstraintName(name);
        }

        public static ReferenceReferenceBuilder<TPrincipal, TRelated> HasForeignKey<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, EntityTypeBuilder entityBuilder, DatabaseFacade database, Expression<Func<TPrincipal, object>> foreignKeyExpression) where TRelated : class where TPrincipal : class
        {
            string name = builder.GetConstraintName(entityBuilder, database, ForeignKeyConstraintPrefix, foreignKeyExpression);
            return builder.HasForeignKey(foreignKeyExpression).HasConstraintName(name);
        }

        public static IndexBuilder HasIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object>> indexExpression) where TEntity : class
        {
            string name = builder.GetConstraintName(database, IndexPrefix, indexExpression);
            return builder.HasIndex(indexExpression).HasDatabaseName(name);
        }

        public static IndexBuilder HasUniqueIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object>> indexExpression) where TEntity : class
        {
            string name = builder.GetConstraintName(database, UniqueConstraintPrefix, indexExpression);
            return builder.HasIndex(indexExpression).HasDatabaseName(name).IsUnique();
        }

        public static KeyBuilder HasPrimaryKeyIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database,Expression<Func<TEntity, object>> keyExpression) where TEntity : class
        {
            string name = builder.GetConstraintName(database, PrimaryKeyConstraintPrefix, keyExpression);
            return builder.HasKey(keyExpression).HasName(name);
        }

        public static PropertyBuilder<long> SetIdentity(this PropertyBuilder<long> builder, DatabaseFacade database, int startsAt = 1, int incrementsBy = 1)
        {
            builder.ValueGeneratedOnAdd();
            switch (database)
            {
                case DatabaseFacade db when db.IsSqlServer():
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsNpgsql():
                    builder.HasIdentityOptions(startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsMySql():
                    break;
                case DatabaseFacade db when db.IsOracle():
                    OraclePropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                default:
                    throw new NotImplementedException("Unknown database provider");
            }
            return builder;
        }

        private static Type ConfigureProvider(IHostEnvironment environment, DbContextOptionsBuilder builder, DbContextSettings settings, DbContextFlavors flavors, bool checkFlavors = true)
        {
            if (environment.IsDevelopment())
            {
                builder.EnableSensitiveDataLogging().EnableDetailedErrors();
            }

            switch (settings.Provider)
            {
                case DbProvider.SqlServer:
                    builder.UseSqlServer(settings.SqlServerConnectionString );
                    return checkFlavors?  (flavors.SqlServer?? throw new ArgumentException("SqlServer flavor is null. Set the correct flavor type.")) : flavors.SqlServer;
                case DbProvider.PostgreSql:
                    builder.UseNpgsql(settings.PostgreSqlConnectionString);
                    return checkFlavors ? (flavors.PostgreSql ?? throw new ArgumentException("PostgreSql flavor is null. Set the correct flavor type.")) : flavors.PostgreSql;
                case DbProvider.MySql:
                    builder.UseMySql(settings.MySqlConnectionString, ServerVersion.AutoDetect(settings.MySqlConnectionString));
                    return checkFlavors ? (flavors.MySql ?? throw new ArgumentException("MySql flavor is null. Set the correct flavor type.")) : flavors.MySql;
                case DbProvider.Oracle:
                    builder.UseOracle(settings.MySqlConnectionString);
                    return checkFlavors ? (flavors.Oracle ?? throw new ArgumentException("Oracle flavor is null. Set the correct flavor type.")) : flavors.Oracle;
                default:
                    throw new NotImplementedException($"Not implemented provider \"{settings.Provider.ToString()}\".");
            }
        }

        public static TCoreDbContext CreateDbContext<TCoreDbContext>(IHostEnvironment environment, DbContextSettings settings, DbContextFlavors flavors, bool checkFlavors = true) where TCoreDbContext : DbContextBase
        {
            if (typeof(TCoreDbContext).Equals(typeof(DbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TCoreDbContext)}\" type parameter cannot be \"{nameof(DbContextBase)}\".");
            }
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            return (TCoreDbContext)Activator.CreateInstance(ConfigureProvider(environment, builder, settings, flavors, checkFlavors), builder.Options)!;
        }


        public static IServiceCollection AddDbContext<TCoreDbContext>(this IServiceCollection services, IHostEnvironment environment, DbContextSettings settings, DbContextFlavors flavors, ServiceLifetime lifetime = ServiceLifetime.Scoped, bool checkFlavors = true)
            where TCoreDbContext : DbContextBase
        {
            if (typeof(TCoreDbContext).Equals(typeof(DbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TCoreDbContext)}\" type parameter cannot be \"{nameof(DbContextBase)}\".");
            }
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            Type dbContextType = ConfigureProvider(environment, builder, settings, flavors, checkFlavors);

            string methodName = null!;
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    methodName = nameof(ServiceCollectionServiceExtensions.AddScoped);
                    break;
                case ServiceLifetime.Transient:
                    methodName = nameof(ServiceCollectionServiceExtensions.AddTransient);
                    break;
                case ServiceLifetime.Singleton:
                    methodName = nameof(ServiceCollectionServiceExtensions.AddSingleton);
                    break;
                default:
                    throw new NotImplementedException("Not implemented lifetime.");
            }

            var methodInfo = typeof(ServiceCollectionServiceExtensions).GetMethods()
                                        .Where(m => m.Name.Equals(methodName))
                                        .Select(m => new
                                        {
                                            Method = m,
                                            Args = m.GetGenericArguments(),
                                            Params = m.GetParameters()
                                        })
                                        .Where(x => x.Args.Length == 2 && x.Params.Length == 2 && "TService".Equals(x.Args[0].Name) && "TImplementation".Equals(x.Args[1].Name))
                                        .First();

            var genericMethod = methodInfo.Method.MakeGenericMethod(new Type[] { typeof(TCoreDbContext), dbContextType });

            var exParameter = Expression.Parameter(typeof(IServiceProvider));
            var exNew = Expression.New(dbContextType.GetConstructor(new Type[] { typeof(DbContextOptions) })!, Expression.Constant(builder.Options));
            var exConvert = Expression.Convert(exNew, dbContextType);
            var exLambda = Expression.Lambda(exConvert, exParameter);

            genericMethod.Invoke(null, new object[] { services, exLambda.Compile() });
            return services;
        }

    }
}

//switch (dbContextType)
//{

//    case var type when typeof(ISqlServerDbContext).IsAssignableFrom(type):
//        Func<IServiceProvider, dbContextType> func = provider => (dbContextType)Activator.CreateInstance(dbContextType, builder.Options)!;
//        genericMethod.Invoke(null, new object[] { services, func });
//        break;
//    case var type when typeof(IPostgreSqlDbContext).IsAssignableFrom(type):
//        Func<IServiceProvider, dbContextType> func2 = (provider => (dbContextType)Activator.CreateInstance(dbContextType, builder.Options)!);
//        genericMethod.Invoke(null, new object[] { services, func2 });
//        break;
//    case var type when typeof(IMySqlDbContext).IsAssignableFrom(type):
//        Func<IServiceProvider, dbContextType> func3 = (provider => (dbContextType)Activator.CreateInstance(dbContextType, builder.Options)!);
//        genericMethod.Invoke(null, new object[] { services, func3 });
//        break;
//    default:
//        throw new NotImplementedException("Not implemented DbContext type.");
//}