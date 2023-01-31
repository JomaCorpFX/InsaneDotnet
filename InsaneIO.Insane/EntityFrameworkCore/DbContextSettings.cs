namespace InsaneIO.Insane.EntityFrameworkCore
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
