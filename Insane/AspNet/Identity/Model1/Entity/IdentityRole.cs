using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class IdentityRole : IdentityRoleBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityUserRecoveryCode, IdentityLog> { 
    
    }

    public abstract class IdentityRoleBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TRecoveryCode, TLog> : IEntity
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
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
        public bool Enabled { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ActiveUntil { get; set; }


        public ICollection<TAccess> Accesses { get; set; } = null!;
        
    }

    
}
