using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1
{
    public class IdentityUser: IdentityUserBase<long, IdentityUser, IdentityRole, IdentityAccess, IdentityUserClaim, IdentityPlatform, IdentitySession, IdentityLog> { } 

    public abstract class IdentityUserBase<TKey, TUser, TRole, TAccess, TUserClaim, TPlatform, TSession, TLog> : IEntity
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
        public string Username { get; set; } = null!;
        public string NormalizedUsername { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string? EmailConfirmationCode { get; set; }
        public DateTimeOffset? EmailConfirmationDeadline { get; set; }
        public bool MobileConfirmed { get; set; }
        public string? MobileConfirmationCode { get; set; }
        public DateTimeOffset? MobileConfirmationDeadline { get; set; }
        public int LoginFailCount { get; set; }
        public DateTimeOffset? LockoutUntil { get; set; }
        public string? Summary { get; set; }
        public string? ProfilePictureUri { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool Enabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string TwoFactorSecretKey { get; set; } = null!;
        public DateTimeOffset? ActiveUntil { get; set; }


        public ICollection<TAccess> Accesses { get; set; } = null!;
        public ICollection<TUserClaim> Claims { get; set; } = null!;
        public ICollection<TSession> Sessions { get; set; } = null!;
        public ICollection<TPlatform> ManagedPlatforms { get; set; } = null!;
    }


}
