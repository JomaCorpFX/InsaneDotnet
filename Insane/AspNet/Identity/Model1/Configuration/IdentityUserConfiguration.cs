using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityUserConfiguration : EntityTypeConfigurationBase<IdentityUser>
    {
        public IdentityUserConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public IdentityUserConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<IdentityUser> builder)
        {    
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).ValueGeneratedOnAdd(Database, builder);
            builder.Property(e => e.Username).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.UniqueId).HasMaxLength(IdentityConstants.IdentifierMaxLength);
            builder.Property(e => e.NormalizedUsername).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.Password).HasMaxLength(IdentityConstants.KeyMaxLength).IsConcurrencyToken();
            builder.Property(e => e.Email).HasMaxLength(IdentityConstants.EmailMaxLength);
            builder.Property(e => e.NormalizedEmail).HasMaxLength(IdentityConstants.EmailMaxLength);
            builder.Property(e => e.Phone).HasMaxLength(IdentityConstants.PhoneMaxLength);
            builder.Property(e => e.Mobile).HasMaxLength(IdentityConstants.PhoneMaxLength);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.Enabled);
            builder.Property(e => e.Active);
            builder.Property(e => e.EmailConfirmed);
            builder.Property(e => e.EmailConfirmationCode);
            builder.Property(e => e.EmailConfirmationDeadline);
            builder.Property(e => e.MobileConfirmed);
            builder.Property(e => e.MobileConfirmationCode);
            builder.Property(e => e.MobileConfirmationDeadline);
            builder.Property(e => e.LoginFailCount);
            builder.Property(e => e.LockoutUntil);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Username);
            builder.HasUniqueIndex(Database, e => e.UniqueId);
            builder.HasUniqueIndex(Database, e => e.Email);
            builder.HasUniqueIndex(Database, e => e.Mobile);

        }
    }
}
