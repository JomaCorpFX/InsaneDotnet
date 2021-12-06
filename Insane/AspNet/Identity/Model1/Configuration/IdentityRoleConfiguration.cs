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
    class IdentityRoleConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : EntityTypeConfigurationBase<TRole>
       where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
    {
        public IdentityRoleConfiguration(DatabaseFacade database, string? schema = null) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<TRole> builder)
        {
            builder.ToTable(Database, Schema);
            builder.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd(Database, builder, startsAt: Constants.IdentityColumnStartValue);
            builder.Ignore(e => e.UniqueId);
            builder.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength).IsConcurrencyToken();
            builder.Property(e => e.Description).IsRequired(false).IsUnicode().HasMaxLength(Constants.DescriptionMaxLength);
            builder.Property(e => e.LogoUri).IsRequired(false).IsUnicode().HasMaxLength(Constants.UriMaxLength);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.Enabled).IsRequired().IsConcurrencyToken().IsConcurrencyToken();
            builder.Property(e => e.ActiveUntil).IsRequired(false).IsConcurrencyToken();

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Name);
        }
    }
}
