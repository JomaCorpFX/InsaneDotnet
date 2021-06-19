using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string UniqueId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string NormalizedUsername { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailConfirmationCode { get; set; } = null!;
        public DateTimeOffset EmailConfirmationDeadline { get; set; }
        public bool MobileConfirmed { get; set; }
        public string MobileConfirmationCode { get; set; } = null!;
        public DateTimeOffset MobileConfirmationDeadline { get; set; }
        public int LoginFailCount { get; set; }
        public DateTimeOffset LockoutUntil { get; set; }
        public ICollection<Permission> Permissions { get; set; } = null!;

    }
}
