﻿using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class UserConfiguration : EntityTypeConfigurationBase<User>
    {
        public UserConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(Database, Schema);
            builder.Property(e => e.Id).SetIdentity(builder, Database, IdentityConstants.IdentityColumnStartValue);
            builder.Property(e => e.Username).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.UniqueId).HasMaxLength(IdentityConstants.IdentifierMaxLength);
            builder.Property(e => e.NormalizedUsername).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.Password).HasMaxLength(IdentityConstants.KeyMaxLength);
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
