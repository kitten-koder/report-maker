using FluentMigrator.Runner;

namespace CostReportMaker.Database.Dialects
{
    public abstract class BootstrapperBase
    {
        protected readonly DatabaseConfig _databaseConfig;
        protected readonly IMigrationRunner _runner;

        protected BootstrapperBase(DatabaseConfig config, IMigrationRunner runner)
        {
            _databaseConfig = config;
            _runner = runner;
        }
    }
}