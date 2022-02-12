using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentitySessionConfiguration : IdentitySessionConfiguration<long>
    {
        public IdentitySessionConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    public class IdentitySessionConfiguration<TKey> : IdentitySessionConfigurationBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>>
        where TKey : IEquatable<TKey>
    {
        public IdentitySessionConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

    public abstract class IdentitySessionConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : EntityTypeConfigurationBase<TSession>
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
        public IdentitySessionConfigurationBase(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<TSession> builder)
        {
            builder.ToTable(Database);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue); ;
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.PlatformId).IsRequired();
            builder.Property(e =>e.UserId).IsRequired();
            builder.Property(e =>e.Jti ).IsUnicode(false).IsRequired().HasMaxLength(Constants.IdentifierMaxLength).IsConcurrencyToken();
            builder.Property(e => e.JwtHash).IsUnicode(false).IsRequired().HasMaxLength(Constants.HashMaxLength).IsConcurrencyToken();
            builder.Property(e => e.RefreshToken).IsUnicode(false).IsRequired().HasMaxLength(Constants.IdentifierMaxLength).IsConcurrencyToken();
            builder.Property(e => e.SessionKey).IsUnicode(false).IsRequired().HasMaxLength(Constants.SaltLength).IsConcurrencyToken();
            builder.Property(e =>  e.ClientDeviceUid).IsUnicode(false).IsRequired().HasMaxLength(Constants.IdentifierMaxLength);
            builder.Property(e => e.ClientUserAgent).IsRequired().HasMaxLength(Constants.SummaryMaxLength);
            builder.Property(e => e.ClientFriendlyName).IsRequired().HasMaxLength(Constants.NameMaxLength);
            builder.Property(e => e.ClientOS).IsRequired().HasMaxLength(Constants.NameMaxLength);
            builder.Property(e => e.ClientTimezone).IsRequired();
            builder.Property(e => e.ClientIP).IsRequired(false).HasMaxLength(Constants.IpMaxLength);
            builder.Property(e =>e.ClientLatitude).IsRequired(false);
            builder.Property(e =>e.ClientLongitude).IsRequired(false);
            builder.Property(e =>e.ExpiresAt).IsRequired().IsConcurrencyToken();
            builder.Property(e =>e.Revoked).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.ActiveUntil).IsRequired(false).IsConcurrencyToken();
            builder.Property(e =>e.CreatedAt).IsRequired();
            builder.Property(e =>e.UpdatedAt).IsRequired().IsConcurrencyToken();
            builder.Property(e => e.Confirmed).IsRequired().IsConcurrencyToken();

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasIndex(Database, e => e.PlatformId);
            builder.HasIndex(Database, e => e.UserId);
            builder.HasUniqueIndex(Database, e => e.Jti);
            builder.HasUniqueIndex(Database, e => e.JwtHash);
            builder.HasUniqueIndex(Database, e => e.RefreshToken);
            builder.HasUniqueIndex(Database, e => e.SessionKey);

            builder.HasOne(e => e.User).WithMany(e => e.Sessions).HasForeignKey(Database, builder, e => e.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Platform).WithMany(e => e.Sessions).HasForeignKey(Database, builder, e => e.PlatformId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
