using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public class SS : DbContextBase, ISqlServerDbContext
    {
        public SS(DbContextOptions options) : base(options)
        {
        }
    }

    public class PG : DbContextBase, IPostgreSqlDbContext
    {
        public PG(DbContextOptions options) : base(options)
        {
        }
    }

    public class MY : DbContextBase, IMySqlDbContext
    {
        public MY(DbContextOptions options) : base(options)
        {
        }
    }

    public class DbContextFlavors
    {

        public static DbContextFlavors CreateInstance<TSqlServerDbContext, TPosgreSqlDbContext, TMySqlDbContext, TOracleDbContext>()
        where TSqlServerDbContext : DbContextBase, ISqlServerDbContext
        where TPosgreSqlDbContext : DbContextBase, IPostgreSqlDbContext
        where TMySqlDbContext : DbContextBase, IMySqlDbContext
        where TOracleDbContext : DbContextBase, IOracleDbContext
        {
            DbContextFlavors flavors = new DbContextFlavors()
            {
                SqlServer = typeof(TSqlServerDbContext),
                PostgreSql = typeof(TPosgreSqlDbContext),
                MySql = typeof(TMySqlDbContext),
                Oracle = typeof(TOracleDbContext)
            };

            return flavors;
        }


        public static DbContextFlavors CreateInstance<TCoreDbContext>(Type[] flavorTypes)
            where TCoreDbContext : DbContextBase
        {
            DbContextFlavors flavors = new DbContextFlavors();
            foreach(var value in flavorTypes)
            {
                switch(value)
                {
                    case Type type when type.IsAssignableFrom(typeof(ISqlServerDbContext)):
                        flavors.SqlServer = value;
                        break;
                    case Type type when type.IsAssignableFrom(typeof(IPostgreSqlDbContext)):
                        flavors.PostgreSql = value;
                        break;
                    case Type type when type.IsAssignableFrom(typeof(IMySqlDbContext)):
                        flavors.MySql = value;
                        break;
                    case Type type when type.IsAssignableFrom(typeof(IOracleDbContext)):
                        flavors.Oracle = value;
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented context type. \"{value.Name}\".");
                }
            }
            return flavors;
        }
        
        private DbContextFlavors()
        {

        }

        public Type SqlServer { private set; get; } = null!;
        public Type PostgreSql { private set; get; } = null!;
        public Type MySql { private set; get; } = null!;
        public Type Oracle { private set; get; } = null!;


    }
}
