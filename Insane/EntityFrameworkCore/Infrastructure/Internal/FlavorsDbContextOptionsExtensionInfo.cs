using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Infrastructure.Internal
{
    public class FlavorsDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
    {
        public FlavorsDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => false;

        public override string LogFragment => $"{nameof(FlavorsDbContextOptionsExtensionInfo)}";

        public override long GetServiceProviderHashCode() => 0;
        

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
            
        }
    }
}
