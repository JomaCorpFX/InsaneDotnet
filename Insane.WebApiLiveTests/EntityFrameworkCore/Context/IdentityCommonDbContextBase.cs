using Insane.AspNet.Identity.Model1;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{
    public abstract class IdentityCommonDbContextBase<TDerivedContext> : IdentityDbContextString<TDerivedContext>
        where TDerivedContext : CoreDbContextBase<TDerivedContext>
    {
        public IdentityCommonDbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Insane");
            base.OnModelCreating(builder);
        }
    }
}
