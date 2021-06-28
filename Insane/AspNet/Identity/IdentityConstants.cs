using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity
{
    public class IdentityConstants
    {
        public const string InsaneIdentityConfigurationName = "InsaneIdentity";
        public const string InsaneIdentityDbSettingsConfigurationName = "DbContextSettings";
        public const string DefaultConfigurationFile = "appsettings.json";
        public const string DefaultSchema = "Identity";
        public const int IdentityColumnStartValue = 10000;
        public const int NameMaxLength = 128;
        public const int EmailMaxLength = 128;
        public const int PhoneMaxLength = 16;
        public const int IdentifierMaxLength = 128;
        public const int HashMaxLength = 128;
        public const int DescriptionMaxLength = 512;
        public const int KeyMaxLength = 128;
        public const int UriMaxLength = 256;
        public const int IpMaxLength = 64;
        public const int AddressMaxLength = 128;

    }
}
