using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
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


        public static DbContextFlavors CreateInstance<TContextBase>(Type[] flavorTypes)
            where TContextBase : DbContextBase
        {
            DbContextFlavors flavors = new DbContextFlavors();
            foreach (var value in flavorTypes)
            {
                if (!value.IsSubclassOf(typeof(TContextBase)))
                {
                    throw new NotImplementedException($"Type {value.Name} is not a subclass of \"{(typeof(TContextBase)).Name}\".");
                }
                switch (value)
                {
                    case Type type when type.GetInterfaces().Contains(typeof(ISqlServerDbContext)):
                        flavors.SqlServer = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IPostgreSqlDbContext)):
                        flavors.PostgreSql = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IMySqlDbContext)):
                        flavors.MySql = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IOracleDbContext)):
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
