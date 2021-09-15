using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class IdentityRole : IEntity
    {
        public long Id { get; set; } 
        public string Name { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset ActiveUntil { get; set; }

        public ICollection<IdentityPermission> Permissions { get; set; } = null!;
    }
}
