using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1.Factory
{
    public abstract class IdentityDbContextFactoryBase<TContext> : CoreDbContextFactoryBase<TContext>
        where TContext : CoreDbContextBase<TContext>
    {
        public const string IdentityConfigurationPath = "Insane:AspNet:Identity:Model1:DbContextSettings";

        public override TContext CreateDbContext(string[] args)
        {
            var argsLst = args.ToList();
            argsLst.Add($"{nameof(ConfigureSettingsParameters.ConfigurationPath)}={IdentityConfigurationPath}");
            return base.CreateDbContext(argsLst.ToArray());
        }
    }
}
