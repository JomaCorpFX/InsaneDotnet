using Insane.AspNet.Identity.Model1;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{

    [CoreDbContext]
    public abstract class IdentityCoreDbContextBase<TDerivedContext> : IdentityDbContextString<TDerivedContext>
        where TDerivedContext : CoreDbContextBase<TDerivedContext>
    {
        public IdentityCoreDbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Insane");
            base.OnModelCreating(builder);
        }
    }
}
