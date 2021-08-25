using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityOrganizationConfiguration : EntityTypeConfigurationBase<IdentityOrganization>
        
    {
        public IdentityOrganizationConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public IdentityOrganizationConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }


        public override void Configure(EntityTypeBuilder<IdentityOrganization> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).ValueGeneratedOnAdd(Database, builder);
            builder.Property(e => e.Name).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.AddressLine1).IsUnicode().HasMaxLength(IdentityConstants.AddressMaxLength);
            builder.Property(e => e.AddresssLine2).IsUnicode().HasMaxLength(IdentityConstants.AddressMaxLength);
            builder.Property(e => e.Email).HasMaxLength(IdentityConstants.EmailMaxLength);
            builder.Property(e => e.Phone).HasMaxLength(IdentityConstants.PhoneMaxLength);
            builder.Property(e => e.LogoUri).HasMaxLength(IdentityConstants.UriMaxLength);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.Active);
            builder.Property(e => e.Enabled);
            builder.Property(e => e.ActiveUntil);
            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Name);
        }

        
    }
}