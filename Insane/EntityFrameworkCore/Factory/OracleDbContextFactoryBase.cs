using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Factory
{
    public abstract class OracleDbContextFactoryBase<TContextBase, TContext> : DbContextFactoryBase<TContextBase, TContext>
        where TContext : TContextBase, IOracleDbContext
        where TContextBase : CoreDbContextBase
    {

    }
}
