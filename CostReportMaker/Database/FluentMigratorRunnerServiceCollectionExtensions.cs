using CostReportMaker.Database;
using CostReportMaker.Database.Migrations;
using CostReportMaker.Properties;
using FluentMigrator.Runner;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentMigratorRunnerServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentMigratorRunnerConfigurator(this IServiceCollection services, DatabaseConfig config)
        {
            var connectionString = config != null ? config.ConnectionString : Settings.Default.DatabaseName;
            int dbType = config != null ? ((int)config.DatabaseType) : Settings.Default.DatabaseType;

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (dbType == 0 || dbType == (int)SupportedDatabases.SQLite)
            {
                services
                    // configure runner
                    .ConfigureRunner(rb => rb
                        .AddSQLite()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(AddUserTable).Assembly).For.Migrations()
                    );
            }
            else if (dbType == ((int)SupportedDatabases.SQLServer))
            {
                services
                    // configure runner
                    .ConfigureRunner(rb => rb
                        .AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(AddUserTable).Assembly).For.Migrations()
                    );
            }
            return services;
        }

        public static IServiceCollection AddFluentMigratorRunnerConfigurator(this IServiceCollection services)
        {
            return AddFluentMigratorRunnerConfigurator(services, null);
        }
    }
}