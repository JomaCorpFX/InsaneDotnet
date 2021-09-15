using Insane.AspNet.Identity.Model1.Context;
using Insane.EntityFrameworkCore;
using Insane.WebApiLiveTest.Factory;
using Insane.WebApiTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Insane.WebApiLiveTest.Factory
{
    public class IdentityMySqlDbContextFactory : IdentityDbContextFactoryBase<IdentityDbContextBase, IdentityMySqlDbContext>
    {
        public override DbContextOptionsBuilderActionFlavors DbContextOptionsBuilderActionFlavors => new DbContextOptionsBuilderActionFlavors()
        {
            MySql = (options) =>
            {
                options.MigrationsAssembly(typeof(Program).Assembly.FullName);
            }
        };

    }
}
