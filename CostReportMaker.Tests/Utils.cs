using CostReportMaker.Database;
using Microsoft.Extensions.DependencyInjection;

namespace CostReportMaker.Tests
{
    public static class Utils
    {
        private static readonly DatabaseConfig _config = new DatabaseConfig()
        {
            DatabaseType = SupportedDatabases.SQLite,
            ConnectionString = "Data Source=TestData2"
        };

        private static ServiceProvider? _appServiceProvider = null;

        public static ServiceProvider CreateServiceProvider()
        {
            if (_appServiceProvider == null)
                _appServiceProvider = new ServiceCollection()
                    .AddFluentMigratorCore()
                    .AddFluentMigratorRunnerConfigurator(GetTestDatabaseConfig())
                    .BuildServiceProvider(false);

            return _appServiceProvider;
        }

        public static DatabaseConfig GetTestDatabaseConfig()
        {
            return _config;
        }
    }
}