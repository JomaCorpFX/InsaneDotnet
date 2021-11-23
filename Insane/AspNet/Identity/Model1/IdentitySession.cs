using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1
{
   
    public class IdentitySession:IdentitySessionBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityLog> { }

    public abstract class IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : IEntity
    where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog>
    {
        public virtual TKey Id { get; set; } = default(TKey)!;

        [NotMapped]
        public string UniqueId { get; set; } = null!;
        public long PlatformId { get; set; }
        public long UserId { get; set; }
        public string Jti { get; set; } = null!;
        public string TokenHash { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string ClientUserAgent { get; set; } = null!;
        public string ClientFriendlyName { get; set; } = null!;
        public string ClientOS { get; set; } = null!;
        public string ClientIP { get; set; } = null!;
        public int ClientTimezone { get; set; }
        public decimal ClientLatitude { get; set; }
        public decimal ClientLongitude { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public bool Revoked { get; set; }


        public TUser User { get; set; } = null!;
        public TPlatform Platform { get; set; } = null!;

    }
}
