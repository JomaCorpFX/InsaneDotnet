using Insane.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFramework
{
    public class DbContextSettings
    {
        public DbProvider Provider { get; set; }
        public string SqlServerConnectionString { get; set; } = null!;
        public string PostgreSqlConnectionString { get; set; } = null!;
        public string MySqlConnectionString { get; set; } = null!;
        public string OracleConnectionString { get; set; } = null!;
    }
}
