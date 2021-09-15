using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Factory
{
    public abstract class SqlServerDbContextFactoryBase<TContextBase, TContext> : DbContextFactoryBase<TContextBase, TContext>
        where TContext : TContextBase, ISqlServerDbContext
        where TContextBase : CoreDbContextBase
    {

    }
}
