using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
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
            Oracle =  oracleDbContextOptionsBuilderAction;
        }

        public DbContextOptionsBuilderActionFlavors()
        {

        }

        public Action<SqlServerDbContextOptionsBuilder> SqlServer { init; get; } = null!;
        public Action<NpgsqlDbContextOptionsBuilder> PostgreSql { init; get; } = null!;
        public Action<MySqlDbContextOptionsBuilder> MySql { init; get; } = null!;
        public Action<OracleDbContextOptionsBuilder> Oracle { init; get; } = null!;
    }
}
