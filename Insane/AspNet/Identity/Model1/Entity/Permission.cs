using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class Permission
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long OrganizationId { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ActiveUntil { get; set; }
        public bool Enabled { get; set; }


        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
