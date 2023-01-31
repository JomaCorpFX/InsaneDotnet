using InsaneIO.Insane.AspNet.Identity.Model1.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Configuration
{
    [RequiresPreviewFeatures]
    public class IdentityUserClaimConfiguration : IdentityUserClaimConfiguration<long>
    {
        public IdentityUserClaimConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public class IdentityUserClaimConfiguration<TKey> : IdentityUserClaimConfigurationBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserClaimConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public abstract class IdentityUserClaimConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : EntityTypeConfigurationBase<TUserClaim>
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
        public IdentityUserClaimConfigurationBase(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<TUserClaim> builder)
        {
            builder.ToTable(Database);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue);
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Type).IsRequired().IsUnicode(false).HasMaxLength(Constants.TypeNameMaxLength);
            builder.Property(e => e.Value).IsRequired().HasMaxLength(Constants.ValueMaxLength);
            builder.Property(e => e.Enabled).IsRequired();
            builder.Property(e => e.ActiveUntil).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => new { e.UserId, e.Type, e.Value });
            builder.HasIndex(Database, e => e.UserId);

            builder.HasOne(e => e.User).WithMany(e => e.Claims).HasForeignKey(Database, builder, e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
