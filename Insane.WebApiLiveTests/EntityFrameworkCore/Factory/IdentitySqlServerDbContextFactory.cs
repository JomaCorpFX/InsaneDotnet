using Insane.AspNet.Identity.Model1.Context;
using Insane.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTests.EntityFrameworkCore.Factory
{
    public class IdentitySqlServerDbContextFactory : DbContextFactoryBase<IdentitySqlServerDbContext>
    {
        public override IdentitySqlServerDbContext CreateDbContext(string[] args)
        {
            ConstructorAdditionalParameters.Add("Identity");
            return base.CreateDbContext(args);
        }
    }
}
