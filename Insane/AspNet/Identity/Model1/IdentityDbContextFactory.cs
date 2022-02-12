using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.AspNet.Identity.Model1
{
    public abstract class IdentityDbContextFactoryBase<TContext> : DbContextFactoryBase<TContext>
        where TContext : CoreDbContextBase<TContext>
    {

        public override TContext CreateDbContext(string[] args)
        {
            ConstructorAdditionalParameters.Add("Insane");
            return base.CreateDbContext(args);
        }
    }
}
