using Insane.AspNet.Identity.Model1.Factory;
using Insane.EntityFrameworkCore;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Factory
{
    [CoreDbContextFactory]
    public class IdentityCoreDbContextFactoryBase<TContext> : IdentityDbContextFactoryBase<TContext>
        where TContext : CoreDbContextBase<TContext>
    {
        public override TContext CreateDbContext(string[] args)
        {
            var argsList = args.ToList();
            argsList.Add($"{nameof(ConfigureSettingsParameters.SecretTypes)}=\"{typeof(Program).AssemblyQualifiedName}\"");
            argsList.Add($"{nameof(ConfigureSettingsParameters.SecretTypes)}=\"{typeof(WeatherForecast).AssemblyQualifiedName}\"");
            return base.CreateDbContext(argsList.ToArray());
        }
    }
}
