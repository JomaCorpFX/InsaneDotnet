using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class RoleConfiguration : EntityTypeConfigurationBase<Role>
    {
        public RoleConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(Database, IdentityConstants.DefaultSchema);

            builder.Property(e => e.Id).SetIdentity(Database, IdentityConstants.IdentityColumnStartValue);
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
