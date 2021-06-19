using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.AspNet.Identity.Model1.Entity
{
    public class Platform
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string LogoUri { get; set; } = null!;
        public bool Active { get; set; }
        public bool Enabled { get; set; } = true;
        public DateTimeOffset ActiveUntil { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
