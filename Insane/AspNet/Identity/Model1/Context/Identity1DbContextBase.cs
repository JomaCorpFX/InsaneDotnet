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
    public class Identity1DbContextBase : DbContextBase
    {
       
        public Identity1DbContextBase(DbContextOptions options) : base(options)
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
            modelBuilder.ApplyConfiguration(new UserConfiguration(Database, IdentityConstants.DefaultSchema));
            modelBuilder.ApplyConfiguration(new RoleConfiguration(Database, IdentityConstants.DefaultSchema));
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration(Database, IdentityConstants.DefaultSchema));
            modelBuilder.ApplyConfiguration(new PlatformConfiguration(Database, IdentityConstants.DefaultSchema));
            modelBuilder.ApplyConfiguration(new PermissionConfiguration(Database, IdentityConstants.DefaultSchema));
            modelBuilder.ApplyConfiguration(new SessionConfiguration(Database, IdentityConstants.DefaultSchema));
        }
    }
}
