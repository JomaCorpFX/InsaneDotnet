using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insane.AspNet.Identity.Model1.Configuration
{
    public class OrganizationConfiguration : EntityTypeConfigurationBase<Organization>
    {
        public OrganizationConfiguration(DatabaseFacade database) : base(database)
        {
        }

        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable(Database, IdentityConstants.DefaultSchema);
            builder.Property(e => e.Id).SetIdentity(Database, IdentityConstants.IdentityColumnStartValue);
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

//public long Id { get; set; }
//public long PersonId { get; set; }
//public string AddressLine1 { get; set; } = null!;
//public string AddresssLine2 { get; set; } = null!;
//public long CityId { get; set; }
//public string Email { get; set; } = null!;
//public string Phone { get; set; } = null!;
//public DateTimeOffset CreatedAt { get; set; }
//public string LogoUri { get; set; } = null!;
//public bool Active { get; set; }
//public bool Enabled { get; set; }