using InsaneIO.Insane.AspNet.Identity.Model1.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Configuration
{
    [RequiresPreviewFeatures]
    public class IdentityPlatformConfiguration : IdentityPlatformConfiguration<long>
    {
        public IdentityPlatformConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public class IdentityPlatformConfiguration<TKey> : IdentityPlatformConfigurationBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>>
        where TKey : IEquatable<TKey>
    {
        public IdentityPlatformConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public abstract class IdentityPlatformConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : EntityTypeConfigurationBase<TPlatform>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRecoveryCode : IdentityUserRecoveryCodeBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
    {
        public IdentityPlatformConfigurationBase(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<TPlatform> builder)
        {
            builder.ToTable(Database);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue);
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.AdminUserId).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.Name).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.Description).IsRequired(false);
            builder.Property(e => e.ApiKey).IsUnicode(false).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.LogoUri).IsRequired(false).HasMaxLength(Constants.UriMaxLength);
            builder.Property(e => e.Type).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.InDevelopment).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.IsServerSide).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.RevokeTokenWhenLogout).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.ContactEmail).IsRequired(false).HasMaxLength(Constants.EmailMaxLength);
            builder.Property(e => e.Enabled).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.ActiveUntil).IsRequired(false).IsConcurrencyToken();
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Name);
            builder.HasUniqueIndex(Database, e => e.ApiKey);
            builder.HasIndex(Database, e => e.AdminUserId);
            builder.HasOne(e => e.AdminUser).WithMany(e => e.ManagedPlatforms).HasForeignKey(e => e.AdminUserId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
