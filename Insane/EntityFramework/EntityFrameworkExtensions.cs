using Insane.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Insane.Cryptography;
using Insane.EntityFramework.MySql.Metadata.Internal;
using Insane.Extensions;
using Insane.EntityFramework;
using Insane.AspNet.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Insane.EntityFramework.ValueGeneration;

namespace Insane.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public const int PostgreSqlIdentifierMaxLength = 63;
        public const int MySqlIdentifierMaxLength = 63;
        public const int SqlServerIdentifierMaxLength = 128;
        public const int OracleIdentifierMaxLength = 128;


        public const string PrimaryKeyConstraintPrefix = "P";
        public const string ForeignKeyConstraintPrefix = "F";
        public const string UniqueConstraintPrefix = "U";
        public const string IndexPrefix = "I";

        private const int IdentifierNameSuffixLength = 5;

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

        private static string GetSchema<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema, DatabaseFacade database)
            where TEntity:class
        {
            if(string.IsNullOrWhiteSpace(schema))
            {
                if(string.IsNullOrWhiteSpace(builder.Metadata.GetSchema()))
                {
                    if(string.IsNullOrWhiteSpace(builder.Metadata.GetDefaultSchema()))
                    {
                        return null!;
                    }
                    return builder.Metadata.GetDefaultSchema();
                }
                return builder.Metadata.GetSchema();
            }
            return schema;
        }
        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string name, string schema, Action<TableBuilder<TEntity>> buildAction) where TEntity : class
        {
            buildAction = buildAction is null ? (builder) => { } : buildAction;
            name = string.IsNullOrWhiteSpace(name) ? typeof(TEntity).GetPrincipalName() : name;
            schema = builder.GetSchema(schema, database);
            builder.ToTable(name, schema, buildAction);
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
            } - IdentifierNameSuffixLength;
            return $"{(name.Length < (maxLength) ? name : name.Substring(0, maxLength)) }_{ HashExtensions.ToHash(name, HexEncoder.Instance).Substring(0, IdentifierNameSuffixLength).ToUpper() }";
        }

        private static string GetPrincipalName(this EntityTypeBuilder builder, DatabaseFacade database)
        {
            string schema = string.IsNullOrWhiteSpace(builder.Metadata.GetSchema()) ? string.Empty : $"{ builder.Metadata.GetSchema()}_";
            switch (database)
            {
                case DatabaseFacade db when db.IsMySql():
                    return $"{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsSqlServer():
                    return $"{schema}{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsNpgsql():
                    return $"{schema}{builder.Metadata.GetTableName()}";
                case DatabaseFacade db when db.IsOracle():
                    return $"{schema}{builder.Metadata.GetTableName()}";
                default:
                    throw new NotImplementedException("Unknown database provider.");
            }
        }

        private static string GetConstraintName<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string prefix, Expression<Func<TEntity, object>> expression) where TEntity : class
        {
            return GetIdentifierName($"{prefix}_{GetPrincipalName(builder, database)}{GetConstraintFieldSegments(expression)}", database);
        }

        private static string GetConstraintName<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, DatabaseFacade database, EntityTypeBuilder entityBuilder, string prefix, Expression<Func<TPrincipal, object>> expression) where TRelated : class where TPrincipal : class
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}{GetConstraintFieldSegments(expression)}", database);
        }

        private static string GetConstraintName<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, DatabaseFacade database, EntityTypeBuilder entityBuilder, string prefix, Expression<Func<TPrincipal, object>> expression) where TPrincipal : class where TRelated : class
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}{GetConstraintFieldSegments(expression)}", database);
        }

        public static ReferenceCollectionBuilder<TRelated, TPrincipal> HasForeignKey<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, DatabaseFacade database, EntityTypeBuilder entityBuilder, Expression<Func<TPrincipal, object>> foreignKeyExpression) where TRelated : class where TPrincipal : class
        {
            string name = builder.GetConstraintName(database, entityBuilder, ForeignKeyConstraintPrefix, foreignKeyExpression);
            return builder.HasForeignKey(foreignKeyExpression).HasConstraintName(name);
        }

        public static ReferenceReferenceBuilder<TPrincipal, TRelated> HasForeignKey<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, DatabaseFacade database, EntityTypeBuilder entityBuilder, Expression<Func<TPrincipal, object>> foreignKeyExpression) where TRelated : class where TPrincipal : class
        {
            string name = builder.GetConstraintName(database, entityBuilder, ForeignKeyConstraintPrefix, foreignKeyExpression);
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

        public static KeyBuilder HasPrimaryKeyIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object>> keyExpression)
            where TEntity : class
        {
            string name = builder.GetConstraintName(database, PrimaryKeyConstraintPrefix, keyExpression);
            return builder.HasKey(keyExpression).HasName(name);
        }

        public static PropertyBuilder<TKey> ValueGeneratedOnAdd<TEntity, TKey>(this PropertyBuilder<TKey> builder, DatabaseFacade database, EntityTypeBuilder<TEntity> entityBuilder, IEncoder? encoder = null)
            where TKey:IEquatable<TKey>
            where TEntity: class
        {
            builder.ValueGeneratedOnAdd().IsRequired();
            if (typeof(TKey).IsIntType())
            {
                (builder as PropertyBuilder<int>)!.SetIdentity(database, entityBuilder, IdentityConstants.IdentityColumnStartValue);
            }

            if (typeof(TKey).IsLongType())
            {
                (builder as PropertyBuilder<long>)!.SetIdentity(database, entityBuilder, IdentityConstants.IdentityColumnStartValue);
            }

            if (typeof(TKey).IsStringType())
            {
                Func<IProperty, IEntityType, ValueGenerator> factory = (property, entityType) =>
                {
                    return new EncoderValueGenerator(property, encoder ?? Base64Encoder.Instance);
                };
               
                (builder as PropertyBuilder<string>)!.ValueGeneratedOnAdd().HasValueGenerator(factory);
            }
            return builder;
        }
                
        public static PropertyBuilder<long> SetIdentity<TEntity>(this PropertyBuilder<long> builder, DatabaseFacade database, EntityTypeBuilder<TEntity> entityBuilder, int startsAt = 1, int incrementsBy = 1)
           where TEntity : class
        {
            switch (database)
            {
                case DatabaseFacade db when db.IsSqlServer():
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsNpgsql():
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(builder, startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsMySql():
                    entityBuilder.HasAnnotation(CustomMySqlAnnotationProvider.AutoincrementAnnotation, startsAt);
                    break;
                case DatabaseFacade db when db.IsOracle():
                    OraclePropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                default:
                    throw new NotImplementedException(database.ProviderName);
            }
            builder.ValueGeneratedOnAdd();
            return builder;
        }

        public static PropertyBuilder<int> SetIdentity<TEntity>(this PropertyBuilder<int> builder, DatabaseFacade database, EntityTypeBuilder<TEntity> entityBuilder, int startsAt = 1, int incrementsBy = 1)
           where TEntity : class
        {
            switch (database)
            {
                case DatabaseFacade db when db.IsSqlServer():
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsNpgsql():
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(builder, startsAt, incrementsBy);
                    break;
                case DatabaseFacade db when db.IsMySql():
                    entityBuilder.HasAnnotation(CustomMySqlAnnotationProvider.AutoincrementAnnotation, startsAt);
                    break;
                case DatabaseFacade db when db.IsOracle():
                    OraclePropertyBuilderExtensions.UseIdentityColumn(builder, startsAt, incrementsBy);
                    break;
                default:
                    throw new NotImplementedException(database.ProviderName);
            }
            builder.ValueGeneratedOnAdd();
            return builder;
        }

        public static Type ConfigureDbProvider<TCoreDbContext>(this DbContextOptionsBuilder builder, DbContextSettings settings, DbContextFlavors<TCoreDbContext> flavors)
            where TCoreDbContext : CoreDbContextBase
        {
            switch (settings.Provider)
            {
                case DbProvider.SqlServer:
                    builder.UseSqlServer(settings.SqlServerConnectionString);
                    return flavors.SqlServer;
                case DbProvider.PostgreSql:
                    builder.UseNpgsql(settings.PostgreSqlConnectionString);
                    return flavors.PostgreSql;
                case DbProvider.MySql:
                    builder.UseMySql(ServerVersion.AutoDetect(settings.MySqlConnectionString), options =>
                    {
                        options.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate, (schema, entity) =>
                        {
                            if(string.IsNullOrWhiteSpace(schema))
                            {
                                return entity;
                            }
                            return $"{schema}.{entity}";

                        });
                    });
                    builder.UseMySql(settings.MySqlConnectionString, ServerVersion.AutoDetect(settings.MySqlConnectionString));
                    return flavors.MySql;
                case DbProvider.Oracle:
                    builder.UseOracle(settings.MySqlConnectionString);
                    return flavors.Oracle;
                default:
                    throw new NotImplementedException($"Not implemented provider \"{settings.Provider}\".");
            }
        }

        public static TCoreDbContext CreateDbContext<TCoreDbContext>(this DbContextSettings settings, DbContextFlavors<TCoreDbContext> flavors, DbContextOptionsBuilder builder)
            where TCoreDbContext : CoreDbContextBase
        {
            if (typeof(TCoreDbContext).Equals(typeof(CoreDbContextBase)))
            {
                throw new ArgumentException($"\"{nameof(TCoreDbContext)}\" type parameter cannot be \"{nameof(CoreDbContextBase)}\".");
            }

            return (TCoreDbContext)Activator.CreateInstance(ConfigureDbProvider(builder, settings, flavors), builder.Options)!;
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