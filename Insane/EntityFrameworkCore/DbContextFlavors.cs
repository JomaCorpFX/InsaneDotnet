using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
{
    public class DbContextFlavors<TContextBase>
        where TContextBase: CoreDbContextBase
    {

        public static DbContextFlavors<TContextBase> CreateInstance<TSqlServerDbContext, TPosgreSqlDbContext, TMySqlDbContext, TOracleDbContext>()
        where TSqlServerDbContext : TContextBase, ISqlServerDbContext
        where TPosgreSqlDbContext : TContextBase, IPostgreSqlDbContext
        where TMySqlDbContext : TContextBase, IMySqlDbContext
        where TOracleDbContext : TContextBase, IOracleDbContext

        {
            DbContextFlavors<TContextBase> flavors = new DbContextFlavors<TContextBase>()
            {
                SqlServer = typeof(TSqlServerDbContext),
                PostgreSql = typeof(TPosgreSqlDbContext),
                MySql = typeof(TMySqlDbContext),
                Oracle = typeof(TOracleDbContext)
            };

            return flavors;
        }


        public static DbContextFlavors<TContextBase> CreateInstance(Type[] flavorTypes)
        {
            
            Type sqlServerType = null!;
            Type postgreSqlType = null!;
            Type mySqlType = null!;
            Type oracleType = null!;
            foreach (var value in flavorTypes)
            {
                if (!value.IsSubclassOf(typeof(TContextBase)))
                {
                    throw new NotImplementedException($"Type {value.Name} is not a subclass of \"{(typeof(TContextBase)).Name}\".");
                }
                switch (value)
                {
                    case Type type when type.GetInterfaces().Contains(typeof(ISqlServerDbContext)):
                        sqlServerType = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IPostgreSqlDbContext)):
                        postgreSqlType = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IMySqlDbContext)):
                        mySqlType = value;
                        break;
                    case Type type when type.GetInterfaces().Contains(typeof(IOracleDbContext)):
                        oracleType = value;
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented context type. \"{value.Name}\".");
                }
            }
            return new DbContextFlavors<TContextBase>()
            {
                SqlServer = sqlServerType,
                PostgreSql = postgreSqlType,
                MySql = mySqlType,
                Oracle = oracleType
            };
        }

        private DbContextFlavors()
        {

        }

        public DbContextFlavors(Type sqlServer, Type postgreSql, Type mySql, Type oracle)
        {
            SqlServer = sqlServer;
            PostgreSql = postgreSql;
            MySql = mySql;
            Oracle = oracle;
        }

        public Type SqlServer { init; get; } = null!;
        public Type PostgreSql { init; get; } = null!;
        public Type MySql { init; get; } = null!;
        public Type Oracle { init; get; } = null!;


    }
}
