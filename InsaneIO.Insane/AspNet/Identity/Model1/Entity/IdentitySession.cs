using System.ComponentModel.DataAnnotations.Schema;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Entity
{

    public class IdentitySessionString : IdentitySession<string> { }
    public class IdentitySessionLong : IdentitySession<long> { }

    public class IdentitySession<TKey> : IdentitySessionBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>> where TKey : IEquatable<TKey> { }

    public abstract class IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : IEntity
    where TKey : IEquatable<TKey>
        where TUser : IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRole : IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TAccess : IdentityAccessBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TUserClaim : IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TPlatform : IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TSession : IdentitySessionBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TRecoveryCode : IdentityUserRecoveryCodeBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
        where TLog : IdentityLogBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog>
    {
        public virtual TKey Id { get; set; } = default(TKey)!;

        [NotMapped]
        public string UniqueId { get; set; } = null!;
        public TKey PlatformId { get; set; } = default(TKey)!;
        public TKey UserId { get; set; } = default(TKey)!;
        public string Jti { get; set; } = null!;
        public string JwtHash { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string SessionKey { get; set; } = null!;
        public string ClientDeviceUid { get; set; } = null!;
        public string ClientUserAgent { get; set; } = null!;
        public string ClientFriendlyName { get; set; } = null!;
        public string ClientOS { get; set; } = null!;
        public string ClientIP { get; set; } = null!;
        public int ClientTimezone { get; set; }
        public decimal? ClientLatitude { get; set; }
        public decimal? ClientLongitude { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public bool Revoked { get; set; }
        public bool Confirmed { get; set; }
        public DateTimeOffset? ActiveUntil { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }


        public TUser User { get; set; } = null!;
        public TPlatform Platform { get; set; } = null!;

    }
}
