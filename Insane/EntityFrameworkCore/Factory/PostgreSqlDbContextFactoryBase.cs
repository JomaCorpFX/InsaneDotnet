using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Factory
{
    public abstract class PostgreSqlDbContextFactoryBase<TContextBase, TContext> : DbContextFactoryBase<TContextBase, TContext>
        where TContext : TContextBase, IPostgreSqlDbContext
        where TContextBase : CoreDbContextBase
    {

    }
}
