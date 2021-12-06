using Insane.EntityFrameworkCore;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityAccessConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : EntityTypeConfigurationBase<TAccess>
       where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
    {
        public IdentityAccessConfiguration(DatabaseFacade database, string? schema = null) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<TAccess> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue);
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.Active).IsRequired();
            builder.Property(e => e.Enabled).IsRequired();
            builder.Property(e => e.ActiveUntil).IsRequired(false);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e=> new { e.UserId, e.RoleId });
            builder.HasIndex(Database, e => e.UserId);
            builder.HasIndex(Database, e => e.RoleId);
            builder.HasOne(e => e.User).WithMany(e => e.Accesses).HasForeignKey(Database, builder, e => e.UserId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
            builder.HasOne(e => e.Role).WithMany(e => e.Accesses).HasForeignKey(Database, builder, e => e.RoleId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
