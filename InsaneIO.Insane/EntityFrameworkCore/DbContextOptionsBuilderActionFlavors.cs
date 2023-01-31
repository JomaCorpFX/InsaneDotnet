using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Oracle.EntityFrameworkCore.Infrastructure;

namespace InsaneIO.Insane.EntityFrameworkCore
{
    public class DbContextOptionsBuilderActionFlavors
    {
        public DbContextOptionsBuilderActionFlavors(Action<SqlServerDbContextOptionsBuilder> sqlServerDbContextOptionsBuilderAction,
                                                Action<NpgsqlDbContextOptionsBuilder> postgreDbContextOptionsBuilderSqlAction,
                                                Action<MySqlDbContextOptionsBuilder> mySqlDbContextOptionsBuilderAction,
                                                Action<OracleDbContextOptionsBuilder> oracleDbContextOptionsBuilderAction)
        {
            SqlServer = sqlServerDbContextOptionsBuilderAction;
            PostgreSql = postgreDbContextOptionsBuilderSqlAction;
            MySql = mySqlDbContextOptionsBuilderAction;
            Oracle = oracleDbContextOptionsBuilderAction;
        }

        public DbContextOptionsBuilderActionFlavors()
        {

        }

        public Action<SqlServerDbContextOptionsBuilder> SqlServer { set; get; } = null!;
        public Action<NpgsqlDbContextOptionsBuilder> PostgreSql { set; get; } = null!;
        public Action<MySqlDbContextOptionsBuilder> MySql { set; get; } = null!;
        public Action<OracleDbContextOptionsBuilder> Oracle { set; get; } = null!;
    }


}
