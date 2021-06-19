using Insane.AspNet.Identity.Model1.Configuration;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class IdentityDbContextBase : DbContextBase
    {
        public IdentityDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration(Database));
            modelBuilder.ApplyConfiguration(new RoleConfiguration(Database));
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration(Database));
            modelBuilder.ApplyConfiguration(new PlatformConfiguration(Database));
            modelBuilder.ApplyConfiguration(new PermissionConfiguration(Database));
            modelBuilder.ApplyConfiguration(new SessionConfiguration(Database));
        }
    }
}
