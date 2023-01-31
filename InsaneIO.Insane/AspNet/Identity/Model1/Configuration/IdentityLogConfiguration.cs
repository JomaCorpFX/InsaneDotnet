using InsaneIO.Insane.AspNet.Identity.Model1.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.Versioning;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Configuration
{
    [RequiresPreviewFeatures]
    public class IdentityLogConfiguration : IdentityLogConfiguration<long>
    {
        public IdentityLogConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public class IdentityLogConfiguration<TKey> : IdentityLogConfigurationBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>>
        where TKey : IEquatable<TKey>
    {
        public IdentityLogConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    [RequiresPreviewFeatures]
    public abstract class IdentityLogConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : EntityTypeConfigurationBase<TLog>
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
        public IdentityLogConfigurationBase(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<TLog> builder)
        {
            builder.ToTable(Database);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue); ;
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.Level).IsRequired();
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Message).IsRequired(false);
            builder.Property(e => e.RelatedData).IsRequired(false);
            builder.Property(e => e.RelatedExceptionStacktrace).IsRequired(false);
            builder.Property(e => e.CreatedAt).IsRequired();


        }
    }
}
