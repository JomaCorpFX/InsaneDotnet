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
    public class IdentityLogConfiguration : IdentityLogConfigurationBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityUserRecoveryCode, IdentityLog>
    {
        public IdentityLogConfiguration(DatabaseFacade database) : base(database)
        {
        }
    }

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
            builder.ToTable(Database, builder.GetSchema(Database));

            builder.Property(e => e.Id).IsRequired();
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.Level).IsRequired();
            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Message).IsRequired(false).IsUnicode();
            builder.Property(e => e.RelatedData).IsRequired(false).IsUnicode();
            builder.Property(e => e.RelatedExceptionStacktrace).IsRequired(false).IsUnicode();
            builder.Property(e => e.CreatedAt).IsRequired();


        }
    }
}
