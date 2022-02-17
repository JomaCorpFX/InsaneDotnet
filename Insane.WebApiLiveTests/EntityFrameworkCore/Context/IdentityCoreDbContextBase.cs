using Insane.AspNet.Identity.Model1;
using Insane.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Context
{

    //public class IDX : IdentityDbContextString<IDX>
    //{
    //    public IDX(DbContextOptions options) : base(options)
    //    {
    //    }
    //}


    //public class IDZCoreDbContextBase<TContext> : IdentityDbContextString<TContext>
    //    where TContext: CoreDbContextBase<TContext>
    //{
    //    public IDZCoreDbContextBase(DbContextOptions options) : base(options)
    //    {
    //    }
    //}


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
