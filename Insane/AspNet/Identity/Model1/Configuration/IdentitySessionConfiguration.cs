using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Insane.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class IdentitySessionConfiguration : EntityTypeConfigurationBase<IdentitySession>
    {
        public IdentitySessionConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public IdentitySessionConfiguration(DatabaseFacade database, string schema) : base(database, schema)
        {
        }

        public override void Configure(EntityTypeBuilder<IdentitySession> builder)
        {
            builder.ToTable(Database, Schema);

            builder.Property(e => e.Id).SetIdentity(Database, builder, IdentityConstants.IdentityColumnStartValue);
            builder.Property(e => e.PermissionId);
            builder.Property(e => e.PlatformId);
            builder.Property(e => e.Jti).HasMaxLength(IdentityConstants.IdentifierMaxLength);
            builder.Property(e => e.TokenHash).HasMaxLength(IdentityConstants.HashMaxLength);
            builder.Property(e => e.RefreshToken).HasMaxLength(IdentityConstants.HashMaxLength);
            builder.Property(e => e.Key).HasMaxLength(IdentityConstants.HashMaxLength);
            builder.Property(e => e.ClientUserAgent).HasMaxLength(IdentityConstants.DescriptionMaxLength);
            builder.Property(e => e.ClientFriendlyName).IsUnicode().HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.ClientOS).HasMaxLength(IdentityConstants.NameMaxLength);
            builder.Property(e => e.ClientIP).HasMaxLength(IdentityConstants.IpMaxLength);
            builder.Property(e => e.ClientTimezone);
            builder.Property(e => e.ClientLatitude);
            builder.Property(e => e.ClientLongitude);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.ExpiresAt);
            builder.Property(e => e.Revoked);

            builder.HasPrimaryKeyIndex(Database, e => e.Id);
            builder.HasUniqueIndex(Database,e => e.Jti);
            builder.HasUniqueIndex(Database, e => e.TokenHash);
            builder.HasUniqueIndex(Database, e => e.RefreshToken);
            builder.HasUniqueIndex(Database, e => e.Key);

            builder.HasIndex(Database, e => e.PermissionId);
            builder.HasIndex(Database, e => e.PlatformId);

            builder.HasOne(e => e.Permission).WithMany(e => e.Sessions).HasForeignKey(Database, builder, e => e.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Platform).WithMany(e => e.Sessions).HasForeignKey(Database, builder, e => e.PlatformId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
