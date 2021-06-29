using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class PermissionConfiguration : EntityTypeConfigurationBase<Permission>
    {
        public PermissionConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).SetIdentity(builder, Database, IdentityConstants.IdentityColumnStartValue);
            builder.Property(e => e.UserId);
            builder.Property(e => e.RoleId);
            builder.Property(e => e.OrganizationId);
            builder.Property(e => e.Active);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.ActiveUntil);
            builder.Property(e => e.Enabled);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database, e => new { e.UserId, e.RoleId, e.OrganizationId });
            builder.HasIndex(Database, e => e.UserId);
            builder.HasIndex(Database, e => e.RoleId);
            builder.HasIndex(Database, e => e.OrganizationId);

            builder.HasOne(e => e.User).WithMany(e => e.Permissions).HasForeignKey(builder, Database, e => e.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Role).WithMany(e => e.Permissions).HasForeignKey(builder, Database, e => e.RoleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Organization).WithMany(e => e.Permissions).HasForeignKey(builder, Database, e => e.OrganizationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

