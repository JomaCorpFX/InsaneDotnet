using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityPlatformConfiguration : EntityTypeConfigurationBase<IdentityPlatform>
    {
        public IdentityPlatformConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public IdentityPlatformConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<IdentityPlatform> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).ValueGeneratedOnAdd(Database, builder);
            builder.Property(e => e.Name).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.Description).IsUnicode().HasMaxLength(IdentityConstants.DescriptionMaxLength);
            builder.Property(e => e.SecretKey).HasMaxLength(IdentityConstants.KeyMaxLength);
            builder.Property(e => e.LogoUri).HasMaxLength(IdentityConstants.UriMaxLength);
            builder.Property(e => e.Active);
            builder.Property(e => e.Enabled);
            builder.Property(e => e.ActiveUntil);
            builder.Property(e => e.CreatedAt);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Name);
            builder.HasUniqueIndex(Database, e => e.SecretKey);

        }
    }
}