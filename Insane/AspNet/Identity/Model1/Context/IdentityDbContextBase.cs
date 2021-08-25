using Insane.AspNet.Identity.Model1.Configuration;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;

namespace Insane.AspNet.Identity.Model1.Context
{

    public abstract class IdentityDbContextBase: CoreDbContextBase
    {

        public IdentityDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<IdentityOrganization> Organizations { get; set; } = null!;
        public DbSet<IdentityPermission> Permissions { get; set; } = null!;
        public DbSet<IdentityPlatform> Platforms { get; set; } = null!;
        public DbSet<IdentityRole> Roles { get; set; } = null!;
        public DbSet<IdentitySession> Sessions { get; set; } = null!;
        public DbSet<IdentityUser> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new IdentityUserConfiguration(Database));
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration(Database));
            modelBuilder.ApplyConfiguration(new IdentityOrganizationConfiguration(Database));
            modelBuilder.ApplyConfiguration(new IdentityPlatformConfiguration(Database));
            modelBuilder.ApplyConfiguration(new IdentityPermissionConfiguration(Database));
            modelBuilder.ApplyConfiguration(new IdentitySessionConfiguration(Database));
        }
    }
}
