using Insane.AspNet.Identity.Model1.Enum;
using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1
{
    public class IdentityPlatform: IdentityPlatformBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityLog> { } 


    public abstract class IdentityPlatformBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : IEntity
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
        public TKey AdminUserId { get; set; } = default(TKey)!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string? LogoUri { get; set; } = null!;
        public PlatformClass Type { get; set; }
        public string? ContactEmail { get; set; } = null!;
        public bool InDevelopment { get; set; }
        public bool CanUseApiKey { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTimeOffset? ActiveUntil { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public ICollection<TSession> Sessions { get; set; } = null!;
        public TUser AdminUser { get; set; } = null!;
    }
}
