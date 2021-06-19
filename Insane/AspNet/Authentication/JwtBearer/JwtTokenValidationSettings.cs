using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Authentication.JwtBearer
{
    public class JwtTokenValidationSettings
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudience { get; set; } = null!;
        public string IssuerSigningKey { get; set; } = null!;
        public double ClockSkewSeconds { get; set; }
        public bool RequireExpirationTime { get; set; }
    }
}
