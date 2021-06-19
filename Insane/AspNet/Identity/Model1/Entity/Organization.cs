using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string AddresssLine2 { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string LogoUri { get; set; } = null!;
        public bool Active { get; set; }
        public bool Enabled { get; set; }
        public DateTimeOffset ActiveUntil { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public ICollection<Permission> Permissions { get; set; } = null!;
    }
}
