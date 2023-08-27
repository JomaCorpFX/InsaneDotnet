using InsaneIO.Insane.EntityFrameworkCore.MySql.Metadata.Internal;
using InsaneIO.Insane.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.Extensions
{
    [RequiresPreviewFeatures]
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

        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string? name = null, Action<TableBuilder<TEntity>>? buildAction = null)
            where TEntity : class, IEntity
        {
            string schema = GetSchema(builder, database);
            name = string.IsNullOrWhiteSpace(name) ? typeof(TEntity).GetPrincipalName() : name;
            string identifier = name;
            if (database.IsMySql())
            {
                identifier = string.IsNullOrWhiteSpace(schema) ? name : $"{schema}.{name}";
            }
            _ = ValidateIdentifierLength(database, identifier);
            _ = buildAction is null ? builder.ToTable(name, schema) : builder.ToTable(name, schema, buildAction);
            return builder;
        }

        internal static string ValidateIdentifierLength(this DatabaseFacade database, string? identifier)
        {
            int identifierMaxLength = GetIdentifierMaxLength(database);
            if (identifier == null) { throw new ArgumentException("Invalid identifier. \"null\" value."); }
            return identifier.Length > identifierMaxLength ?
                    throw new ArgumentOutOfRangeException($"The maximum length of an identifier for provider \"{database.ProviderName}\" is {identifierMaxLength}. Your identifier is \"{identifier}\"")
                    : identifier;
        }

        internal static string GetSchema<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database)
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(builder.Metadata.GetSchema()))
            {
                if (string.IsNullOrWhiteSpace(builder.Metadata.GetDefaultSchema()))
                {
                    return null!;
                }
                return ValidateIdentifierLength(database, builder.Metadata.GetDefaultSchema());
            }
            return ValidateIdentifierLength(database, builder.Metadata.GetSchema());

        }

        internal static string GetConstraintFieldSegments<TEntity>(Expression<Func<TEntity, object?>> property) where TEntity : class
        {
            StringBuilder ret = new();
            property.GetExpressionReturnMembersNames().ForEach(value =>
            {
                ret.Append($"_{value}");
            });
            return ret.ToString();
        }

        public static DbProvider GetDbProvider(this DatabaseFacade database)
        {
            return database switch
            {
                DatabaseFacade db when db.IsSqlServer() => DbProvider.SqlServer,
                DatabaseFacade db when db.IsNpgsql() => DbProvider.PostgreSql,
                DatabaseFacade db when db.IsMySql() => DbProvider.MySql,
                DatabaseFacade db when db.IsOracle() => DbProvider.Oracle,
                _ => throw new NotImplementedException("Unknown database provider")
            };
        }

        public static DbProvider GetDbProvider<TContext>()
            where TContext : CoreDbContextBase
        {
            return CoreDbContextBase.GetImplementedDbProviderInterface(typeof(TContext), true) switch
            {
                var type when type is not null && type.Equals(typeof(ISqlServerDbContext)) => DbProvider.SqlServer,
                var type when type is not null && type.Equals(typeof(IPostgreSqlDbContext)) => DbProvider.PostgreSql,
                var type when type is not null && type.Equals(typeof(IMySqlDbContext)) => DbProvider.MySql,
                var type when type is not null && type.Equals(typeof(IOracleDbContext)) => DbProvider.Oracle,
                _ => throw new NotImplementedException("Unknown database provider")
            };
        }

        internal static int GetIdentifierMaxLength(DatabaseFacade database)
        {
            return database switch
            {
                DatabaseFacade db when db.GetDbProvider() is DbProvider.SqlServer => SqlServerIdentifierMaxLength,
                DatabaseFacade db when db.GetDbProvider() is DbProvider.PostgreSql => PostgreSqlIdentifierMaxLength,
                DatabaseFacade db when db.GetDbProvider() is DbProvider.MySql => MySqlIdentifierMaxLength,
                DatabaseFacade db when db.GetDbProvider() is DbProvider.Oracle => OracleIdentifierMaxLength,
                _ => throw new NotImplementedException("Unknown database provider")
            };
        }
        internal static string GetIdentifierName(string name, DatabaseFacade database)
        {
            var maxLength = GetIdentifierMaxLength(database) - IdentifierNameSuffixLength;
            return $"{(name.Length < (maxLength) ? name : name.Substring(0, maxLength)) }_{ HashExtensions.ComputeEncodedHash(name, HexEncoder.DefaultInstance).Substring(0, IdentifierNameSuffixLength).ToUpper() }";
        }

        internal static string GetPrincipalName<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database)
            where TEntity : class, IEntity
        {
            string schema = GetSchema(builder, database) ?? string.Empty;
            return database.GetDbProvider() switch
            {
                DbProvider.MySql => $"{builder.Metadata.GetTableName()}",
                DbProvider.SqlServer => $"{schema}{builder.Metadata.GetTableName()}",
                DbProvider.PostgreSql => $"{schema}{builder.Metadata.GetTableName()}",
                DbProvider.Oracle => $"{schema}{builder.Metadata.GetTableName()}",
                _ => throw new NotImplementedException("Unknown database provider.")
            };
        }

        internal static string GetConstraintName<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, string prefix, Expression<Func<TEntity, object?>> expression) where TEntity : class, IEntity
        {
            return GetIdentifierName($"{prefix}_{GetPrincipalName(builder, database)}{GetConstraintFieldSegments(expression)}", database);
        }

        internal static string GetConstraintName<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, DatabaseFacade database, EntityTypeBuilder<TPrincipal> entityBuilder, string prefix, Expression<Func<TPrincipal, object?>> expression) where TRelated : class, IEntity where TPrincipal : class, IEntity
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}{GetConstraintFieldSegments(expression)}", database);
        }

        internal static string GetConstraintName<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, DatabaseFacade database, EntityTypeBuilder<TPrincipal> entityBuilder, string prefix, Expression<Func<TPrincipal, object?>> expression) where TPrincipal : class, IEntity where TRelated : class, IEntity
        {
            return GetIdentifierName($"{prefix}_{entityBuilder.GetPrincipalName(database)}{GetConstraintFieldSegments(expression)}", database);
        }

        public static ReferenceCollectionBuilder<TRelated, TPrincipal> HasForeignKey<TRelated, TPrincipal>(this ReferenceCollectionBuilder<TRelated, TPrincipal> builder, DatabaseFacade database, EntityTypeBuilder<TPrincipal> entityBuilder, Expression<Func<TPrincipal, object?>> foreignKeyExpression) where TRelated : class, IEntity where TPrincipal : class, IEntity
        {
            string name = builder.GetConstraintName(database, entityBuilder, ForeignKeyConstraintPrefix, foreignKeyExpression);
            return builder.HasForeignKey(foreignKeyExpression).HasConstraintName(name);
        }

        public static ReferenceReferenceBuilder<TPrincipal, TRelated> HasForeignKey<TPrincipal, TRelated>(this ReferenceReferenceBuilder<TPrincipal, TRelated> builder, DatabaseFacade database, EntityTypeBuilder<TPrincipal> entityBuilder, Expression<Func<TPrincipal, object?>> foreignKeyExpression)
            where TRelated : class, IEntity where TPrincipal : class, IEntity
        {
            string name = builder.GetConstraintName(database, entityBuilder, ForeignKeyConstraintPrefix, foreignKeyExpression);
            return builder.HasForeignKey(foreignKeyExpression).HasConstraintName(name);
        }

        public static IndexBuilder HasIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object?>> indexExpression)
            where TEntity : class, IEntity
        {
            string name = builder.GetConstraintName(database, IndexPrefix, indexExpression);
            return builder.HasIndex(indexExpression).HasDatabaseName(name);
        }

        public static IndexBuilder HasUniqueIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object?>> indexExpression)
            where TEntity : class, IEntity
        {
            string name = builder.GetConstraintName(database, UniqueConstraintPrefix, indexExpression);
            return builder.HasIndex(indexExpression).HasDatabaseName(name).IsUnique();
        }

        public static KeyBuilder HasPrimaryKeyIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, DatabaseFacade database, Expression<Func<TEntity, object?>> keyExpression)
            where TEntity : class, IEntity
        {
            string name = builder.GetConstraintName(database, PrimaryKeyConstraintPrefix, keyExpression);
            return builder.HasKey(keyExpression).HasName(name);
        }

        public static PropertyBuilder<TKey> ValueGeneratedOnAdd<TEntity, TKey>(this PropertyBuilder<TKey> builder, DatabaseFacade database, EntityTypeBuilder<TEntity> entityBuilder, IEncoder? encoder = null, int startsAt = 1, int incrementsBy = 1)
            where TKey : IEquatable<TKey>
            where TEntity : class
        {

            if (typeof(TKey).IsIntType() || typeof(TKey).IsLongType())
            {
                builder.SetIdentity(database, entityBuilder, startsAt, incrementsBy);
                builder.ValueGeneratedOnAdd();
            }

            if (typeof(TKey).IsStringType())
            {
                Func<IProperty, IEntityType, ValueGenerator> factory = (property, entityType) =>
                {
                    return new EncoderValueGenerator(property, encoder ?? Base64Encoder.DefaultInstance);
                };

                builder.ValueGeneratedOnAdd().HasValueGenerator(factory);
            }
            return builder;
        }

        public static PropertyBuilder<TKey> SetIdentity<TEntity, TKey>(this PropertyBuilder<TKey> propertyBuilder, DatabaseFacade database, EntityTypeBuilder<TEntity> entityBuilder, int startsAt = 1, int incrementsBy = 1)
           where TEntity : class
           where TKey : IEquatable<TKey>
        {
            switch (database.GetDbProvider())
            {
                case DbProvider.SqlServer:
                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(propertyBuilder, startsAt, incrementsBy);
                    break;
                case DbProvider.PostgreSql:
                    NpgsqlPropertyBuilderExtensions.HasIdentityOptions(propertyBuilder, startsAt, incrementsBy);
                    break;
                case DbProvider.MySql:
                    entityBuilder.HasAnnotation(CustomMySqlAnnotationProvider.AutoincrementAnnotation, startsAt);
                    break;
                case DbProvider.Oracle:
                    OraclePropertyBuilderExtensions.UseIdentityColumn(propertyBuilder, startsAt, incrementsBy);
                    break;
                default:
                    throw new NotImplementedException($"Not implemented provider \"{database.ProviderName}\".");
            }
            return propertyBuilder;
        }


        public static DbContextOptionsBuilder<TContext> ConfigureDbContextProviderOptions<TContext>(this DbContextSettings settings, Action<DbContextOptionsBuilder<TContext>>? dbContextOptionsAction, DbContextOptionsBuilderActionFlavors? actionFlavors)
            where TContext : CoreDbContextBase
        {
            DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>();
            if (dbContextOptionsAction != null)
            {
                dbContextOptionsAction?.Invoke(builder);
            }

            switch (GetDbProvider<TContext>())
            {
                case DbProvider.SqlServer:
                    builder.UseSqlServer(settings.SqlServerConnectionString, actionFlavors?.SqlServer);
                    break;
                case DbProvider.PostgreSql:
                    builder.UseNpgsql(settings.PostgreSqlConnectionString, actionFlavors?.PostgreSql);
                    break;
                case DbProvider.MySql:
                    builder.UseMySql(ServerVersion.AutoDetect(settings.MySqlConnectionString), options =>
                    {
                        options.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Translate, (schema, entity) =>
                        {
                            if (string.IsNullOrWhiteSpace(schema))
                            {
                                return entity;
                            }
                            return $"{schema}.{entity}";

                        });
                    });
                    builder.UseMySql(settings.MySqlConnectionString, ServerVersion.AutoDetect(settings.MySqlConnectionString), actionFlavors?.MySql);
                    break;
                case DbProvider.Oracle:
                    builder.UseOracle(settings.OracleConnectionString, actionFlavors?.Oracle);
                    break;
                default:
                    throw new NotImplementedException($@"Not implemented DbProvider ""{settings.Provider}"".");
            }
            return builder;
        }


        public static TEntity Protect<TEntity>(this TEntity entity) where TEntity : class, IEntity, IEntityProtector<TEntity>
        {
            entity.Protect(entity);
            return entity;
        }

        public static TEntity Unprotect<TEntity>(this TEntity entity) where TEntity : class, IEntity, IEntityProtector<TEntity>
        {
            entity.Unprotect(entity);
            return entity;
        }

        public static TEntity Protect<TEntity>(this TEntity entity, IEntityProtector<TEntity> protector) where TEntity : class, IEntity
        {
            protector.Protect(entity);
            return entity;
        }

        public static TEntity Unprotect<TEntity>(this TEntity entity, IEntityProtector<TEntity> protector) where TEntity : class, IEntity
        {
            protector.Unprotect(entity);
            return entity;
        }

        public static TEntity Protect<TEntity>(this IEntityProtector<TEntity> protector, TEntity entity) where TEntity : class, IEntity
        {
            protector.Protect(entity);
            return entity;
        }

        public static TEntity Unprotect<TEntity>(this IEntityProtector<TEntity> protector, TEntity entity) where TEntity : class, IEntity
        {
            protector.Unprotect(entity);
            return entity;
        }

    }
}




