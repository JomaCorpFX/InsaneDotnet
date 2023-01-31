using System.Runtime.Versioning;

namespace InsaneIO.Insane.AspNet.Identity.Model1.Factory
{
    [RequiresPreviewFeatures]
    public abstract class IdentityDbContextFactoryBase<TContext> : CoreDbContextFactoryBase<TContext>
        where TContext : CoreDbContextBase
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
