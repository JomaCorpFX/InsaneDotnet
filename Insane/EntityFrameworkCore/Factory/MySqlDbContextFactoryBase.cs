using Microsoft.EntityFrameworkCore.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Factory
{
    public abstract class MySqlDbContextFactoryBase<TContextBase, TContext> : DbContextFactoryBase<TContextBase, TContext>
        where TContext : TContextBase, IMySqlDbContext
        where TContextBase : CoreDbContextBase
    {

    }
}
