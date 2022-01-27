using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class IdentityUserClaim:IdentityUserClaimBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityUserRecoveryCode, IdentityLog> { }

    public abstract class IdentityUserClaimBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : IEntity
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
        public TKey Id { get; set; } = default(TKey)!;

        [NotMapped]
        public string UniqueId { get; set; } = null!;
        public TKey UserId { get; set; } = default(TKey)!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset? ActiveUntil { get; set; }


        public TUser User { get; set; } = null!;
    }

}
