using InsaneIO.Insane.AspNet.Identity.Model1.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Entity
{
    public class IdentityPlatformString : IdentityPlatform<string> { }
    public class IdentityPlatformLong : IdentityPlatform<long> { }

    public class IdentityPlatform<TKey> : IdentityPlatformBase<TKey, IdentityUser<TKey>, IdentityRole<TKey>, IdentityAccess<TKey>, IdentityUserClaim<TKey>, IdentityPlatform<TKey>, IdentitySession<TKey>, IdentityUserRecoveryCode<TKey>, IdentityLog<TKey>> where TKey : IEquatable<TKey> { }

    public abstract class IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : IEntity
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
        public TKey AdminUserId { get; set; } = default(TKey)!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
        public PlatformClass Type { get; set; }
        public string? ContactEmail { get; set; } = null!;
        public bool InDevelopment { get; set; }
        public bool IsServerSide { get; set; }
        public bool RevokeTokenWhenLogout { get; set; }
        public RememberDeviceStrategy RememberDeviceStrategy { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTimeOffset? ActiveUntil { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public ICollection<TSession> Sessions { get; set; } = null!;
        public TUser AdminUser { get; set; } = null!;
    }
}
