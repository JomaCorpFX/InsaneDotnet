using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset ActiveUntil { get; set; }

        public ICollection<Permission> Permissions { get; set; } = null!;
    }
}
