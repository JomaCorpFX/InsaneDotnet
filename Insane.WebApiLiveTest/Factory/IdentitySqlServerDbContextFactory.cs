using Insane.AspNet.Identity.Model1.Context;
using Insane.EntityFrameworkCore;
using Insane.WebApiTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTest.Factory
{
    public class IdentitySqlServerDbContextFactory : IdentityDbContextFactoryBase<IdentityDbContextBase, IdentitySqlServerDbContext>
    {
        public override DbContextOptionsBuilderActionFlavors DbContextOptionsBuilderActionFlavors => new DbContextOptionsBuilderActionFlavors()
        {
            SqlServer = (options) =>
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
            }
        };
    }
}
