using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1
{
    public class Constants
    {
        public const string InsaneIdentityConfigurationName = "InsaneIdentity";
        public const string InsaneIdentityDbSettingsConfigurationPath = "InsaneIdentity:DbContextSettings";
        public const string DefaultConfigurationFile = "appsettings.json";
        public const string DefaultSchema = "Identity";

        public const int IdentityColumnStartValue = 10000;
        public const int UsernameMaxLength = 128;
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
        public const int GuidMaxLength = 36;
        public const int PasswordInfoMaxLength = 4096;
        public const int SummaryMaxLength = 1024;

        //TODO: █ REVISAR https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/
        //TODO: █ REVISAR https://www.youtube.com/watch?v=6Go46VRs7hI
    }
}
