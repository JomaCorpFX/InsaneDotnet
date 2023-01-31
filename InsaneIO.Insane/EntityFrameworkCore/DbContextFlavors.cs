namespace InsaneIO.Insane.EntityFrameworkCore
{
    public class DbContextFlavors<TDbContextBase>
        where TDbContextBase : CoreDbContextBase
    {

        public static DbContextFlavors<TDbContextBase> CreateInstance<TSqlServerDbContext, TPosgreSqlDbContext, TMySqlDbContext, TOracleDbContext>()
        where TSqlServerDbContext : TDbContextBase, ISqlServerDbContext
        where TPosgreSqlDbContext : TDbContextBase, IPostgreSqlDbContext
        where TMySqlDbContext : TDbContextBase, IMySqlDbContext
        where TOracleDbContext : TDbContextBase, IOracleDbContext
        {
            DbContextFlavors<TDbContextBase> flavors = new DbContextFlavors<TDbContextBase>()
            {
                SqlServer = typeof(TSqlServerDbContext),
                PostgreSql = typeof(TPosgreSqlDbContext),
                MySql = typeof(TMySqlDbContext),
                Oracle = typeof(TOracleDbContext)
            };
            return flavors;
        }


        public static DbContextFlavors<TDbContextBase> CreateInstance(Type[] flavorTypes)
        {

            Type sqlServerType = null!;
            Type postgreSqlType = null!;
            Type mySqlType = null!;
            Type oracleType = null!;
            foreach (var value in flavorTypes)
            {
                if (!value.IsSubclassOf(typeof(TDbContextBase)))
                {
                    throw new NotImplementedException($"Type {value.Name} is not a subclass of \"{(typeof(TDbContextBase)).Name}\".");
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
            return new DbContextFlavors<TDbContextBase>()
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
