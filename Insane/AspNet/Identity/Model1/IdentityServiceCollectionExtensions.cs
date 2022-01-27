using Insane.AspNet.Identity.Model1;
using Insane.AspNet.Identity.Model1.Configuration;
using Insane.AspNet.Identity.Model1.Context;
using Insane.AspNet.Identity.Model1.Entity;
using Insane.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {


        public static IServiceCollection AddIdentity<TContext, TKey
            , TUser, TRole
            , TAccess, TUserClaim
            , TPlatform, TSession
            , TRecoveryCode, TLog
            , TUserConfiguration, TRoleConfiguration
            , TAccessConfiguration, TUserClaimConfiguration
            , TPlatformConfiguration, TSessionConfiguration
            , TRecoveryCodeConfiguration, TLogConfiguration, TOptions>
            (this IServiceCollection services, string name, Action<TOptions> identityOptionsAction, Action<DbContextOptions<TContext>> dbcontextOptionsAction, Action<JwtBearerOptions> jwtBearerOptionsAction)

            where TContext : IdentityDbContextBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog,
                TUserConfiguration, TRoleConfiguration, TAccessConfiguration, TUserClaimConfiguration, TPlatformConfiguration, TSessionConfiguration, TRecoveryCodeConfiguration, TLogConfiguration>
            where TKey : IEquatable<TKey>
            where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TRecoveryCode : IdentityUserRecoveryCodeBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TUserConfiguration : IdentityUserConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TRoleConfiguration : IdentityRoleConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TAccessConfiguration : IdentityAccessConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TUserClaimConfiguration : IdentityUserClaimConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TPlatformConfiguration : IdentityPlatformConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TSessionConfiguration : IdentitySessionConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TRecoveryCodeConfiguration : IdentityUserRecoveryCodeConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TLogConfiguration : IdentityLogConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
            where TOptions : class
        {
            //services.AddDbContext()
            services.AddHttpContextAccessor();
            services.Configure(name, identityOptionsAction);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptionsAction);
            //services.AddDbContextFactory((serviceProvider, dbContextOptionsBuilder//
            return services;
        }

        


        
    }


}




//public static IServiceCollection AddIdentity(this IServiceCollection services, Action<IdentityOptions> identityOptionsAction, Action<DbContextOptions<IdentityDbContext>> contextOptionsAction, Action<JwtBearerOptions> jwtBearerOptionsAction)
//{
//    return services.AddIdentity<IdentityDbContext, long
//        , IdentityUser, IdentityRole
//        , IdentityAccess, IdentityUserClaim
//        , IdentityPlatform, IdentitySession
//        , IdentityUserRecoveryCode, IdentityLog
//        , IdentityUserConfiguration, IdentityRoleConfiguration
//        , IdentityAccessConfiguration, IdentityUserClaimConfiguration
//        , IdentityPlatformConfiguration, IdentitySessionConfiguration
//        , IdentityUserRecoveryCodeConfiguration, IdentityLogConfiguration, IdentityOptions>(identityOptionsAction, jwtBearerOptionsAction);
//}

//public static IServiceCollection AddIdentity(this IServiceCollection services, string optionsName, Action<IdentityOptions> identityOptionsAction, Action<JwtBearerOptions> jwtBearerOptionsAction)
//{

//    return services.AddIdentity<IdentityDbContext, long
//        , IdentityUser, IdentityRole
//        , IdentityAccess, IdentityUserClaim
//        , IdentityPlatform, IdentitySession
//        , IdentityUserRecoveryCode, IdentityLog
//        , IdentityUserConfiguration, IdentityRoleConfiguration
//        , IdentityAccessConfiguration, IdentityUserClaimConfiguration
//        , IdentityPlatformConfiguration, IdentitySessionConfiguration
//        , IdentityUserRecoveryCodeConfiguration, IdentityLogConfiguration, IdentityOptions>(optionsName, identityOptionsAction, jwtBearerOptionsAction);
//}

//public static IServiceCollection AddIdentity<TContext, TKey
//    , TUser, TRole
//    , TAccess, TUserClaim
//    , TPlatform, TSession
//    , TRecoveryCode, TLog
//    , TUserConfiguration, TRoleConfiguration
//    , TAccessConfiguration, TUserClaimConfiguration
//    , TPlatformConfiguration, TSessionConfiguration
//    , TRecoveryCodeConfiguration, TLogConfiguration, TOptions>
//    (this IServiceCollection services, Action<TOptions> identityOptionsAction, Action<JwtBearerOptions> jwtBearerOptionsAction)

//    where TContext : IdentityDbContextBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog,
//        TUserConfiguration, TRoleConfiguration, TAccessConfiguration, TUserClaimConfiguration, TPlatformConfiguration, TSessionConfiguration, TRecoveryCodeConfiguration, TLogConfiguration>
//    where TKey : IEquatable<TKey>
//    where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TRecoveryCode : IdentityUserRecoveryCodeBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TUserConfiguration : IdentityUserConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TRoleConfiguration : IdentityRoleConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TAccessConfiguration : IdentityAccessConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TUserClaimConfiguration : IdentityUserClaimConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TPlatformConfiguration : IdentityPlatformConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TSessionConfiguration : IdentitySessionConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TRecoveryCodeConfiguration : IdentityUserRecoveryCodeConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TLogConfiguration : IdentityLogConfigurationBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
//    where TOptions : class
//{
//    return services.AddIdentity<TContext, TKey
//    , TUser, TRole
//    , TAccess, TUserClaim
//    , TPlatform, TSession
//    , TRecoveryCode, TLog
//    , TUserConfiguration, TRoleConfiguration
//    , TAccessConfiguration, TUserClaimConfiguration
//    , TPlatformConfiguration, TSessionConfiguration
//    , TRecoveryCodeConfiguration, TLogConfiguration, TOptions>(string.Empty, identityOptionsAction, jwtBearerOptionsAction);
//}