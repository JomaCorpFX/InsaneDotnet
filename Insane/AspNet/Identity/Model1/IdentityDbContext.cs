using Insane.AspNet.Identity.Model1.Configuration;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Context
{

    
    public class IdentityDbContext : IdentityDbContextBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityUserRecoveryCode, IdentityLog,
        IdentityUserConfiguration, IdentityRoleConfiguration, IdentityAccessConfiguration, IdentityUserClaimConfiguration, IdentityPlatformConfiguration, IdentitySessionConfiguration, IdentityUserRecoveryCodeConfiguration, IdentityLogConfiguration>
    {
        public IdentityDbContext(DbContextOptions options, string? defaultSchema = null) : base(options, defaultSchema)
        {
        }
    }

    public abstract class IdentityDbContextBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog, 
        TUserConfiguration, TRoleConfiguration, TAccessConfiguration, TUserClaimConfiguration, TPlatformConfiguration, TSessionConfiguration, TRecoveryCodeConfiguration, TLogConfiguration> : CoreDbContextBase
       where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRecoveryCode : IdentityUserRecoveryCodeBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TUserConfiguration: IdentityUserConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRoleConfiguration : IdentityRoleConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TAccessConfiguration : IdentityAccessConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TUserClaimConfiguration : IdentityUserClaimConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TPlatformConfiguration : IdentityPlatformConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TSessionConfiguration : IdentitySessionConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRecoveryCodeConfiguration : IdentityUserRecoveryCodeConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TLogConfiguration : IdentityLogConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
    {

        public DbSet<TUser> Users { get; set; } = null!;
        public DbSet<TRole> Roles { get; set; } = null!;
        public DbSet<TAccess> Accesses { get; set; } = null!;
        public DbSet<TUserClaim> UserClaims { get; set; } = null!;
        public DbSet<TPlatform> Platforms { get; set; } = null!;
        public DbSet<TSession> Sessions { get; set; } = null!;
        public DbSet<TRecoveryCode> RecoveryCodes { get; set; } = null!;
        public DbSet<TLog> Logs { get; set; } = null!;

        public IdentityDbContextBase(DbContextOptions options,string? schema = null) : base(options,schema )
        {
            Schema = schema;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
            builder.ApplyConfiguration((TUserConfiguration)Activator.CreateInstance(typeof(TUserConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TRoleConfiguration)Activator.CreateInstance(typeof(TRoleConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TAccessConfiguration)Activator.CreateInstance(typeof(TAccessConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TUserClaimConfiguration)Activator.CreateInstance(typeof(TUserClaimConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TPlatformConfiguration)Activator.CreateInstance(typeof(TPlatformConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TSessionConfiguration)Activator.CreateInstance(typeof(TSessionConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TRecoveryCodeConfiguration)Activator.CreateInstance(typeof(TRecoveryCodeConfiguration), new object[] { Database })!);
            builder.ApplyConfiguration((TLogConfiguration)Activator.CreateInstance(typeof(TLogConfiguration), new object[] { Database })!);
        }
    }
}
