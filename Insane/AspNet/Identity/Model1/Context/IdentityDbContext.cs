using Insane.AspNet.Identity.Model1.Configuration;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{
    public class IdentityDbContext : IdentityDbContextBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityLog>
    {
        public IdentityDbContext(DbContextOptions options, string? defaultSchema = null) : base(options, defaultSchema)
        {
        }
    }

    public abstract class IdentityDbContextBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : CoreDbContextBase
       where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
    {
        private readonly string? Schema;

        public DbSet<TUser> Users { get; set; } = null!;
        public DbSet<TRole> Roles { get; set; } = null!;
        public DbSet<TAccess> Accesses { get; set; } = null!;
        public DbSet<TUserClaim> UserClaims { get; set; } = null!;
        public DbSet<TPlatform> Platforms { get; set; } = null!;
        public DbSet<TSession> Sessions { get; set; } = null!;
        public DbSet<TLog> Logs { get; set; } = null!;

        public IdentityDbContextBase(DbContextOptions options, string? defaultSchema = null) : base(options)
        {
            Schema = defaultSchema;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
            builder.ApplyConfiguration(new IdentityUserConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentityRoleConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentityAccessConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentityUserClaimConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentityPlatformConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentitySessionConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
            builder.ApplyConfiguration(new IdentityLogConfiguration<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>(Database));
        }
    }
}
