using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class IdentityPermission: IEntity
    {
        public long Id { get; set; } 
        public long UserId { get; set; } 
        public long RoleId { get; set; } 
        public long OrganizationId { get; set; } 
        public bool Active { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ActiveUntil { get; set; }
        public bool Enabled { get; set; }


        public IdentityUser User { get; set; } = null!;
        public IdentityRole Role { get; set; } = null!;
        public IdentityOrganization Organization { get; set; } = null!;
        public ICollection<IdentitySession> Sessions { get; set; } = null!;
    }
}
