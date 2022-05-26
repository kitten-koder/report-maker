using System.ComponentModel;

namespace CostReportMaker.Database
{
    public class DatabaseConfig
    {
        public SupportedDatabases DatabaseType { get; set; } = SupportedDatabases.SQLite;
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// Supported Database
    /// </summary>
    public enum SupportedDatabases
    {
        [Description("sqlite")]
        SQLite,

        [Description("sqlserver")]
        SQLServer
    }
}