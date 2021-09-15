using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentityRoleConfiguration : EntityTypeConfigurationBase<IdentityRole>
    {
        public IdentityRoleConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public IdentityRoleConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).ValueGeneratedOnAdd(Database, builder);
            builder.Property(e => e.Name).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.Active);
            builder.Property(e => e.Enabled);
            builder.Property(e => e.ActiveUntil);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => e.Name);
        }
    }
}
