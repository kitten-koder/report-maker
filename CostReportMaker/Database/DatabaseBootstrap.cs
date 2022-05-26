using CostReportMaker.Database.Dialects;
using FluentMigrator.Runner;
using System;

namespace CostReportMaker.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly IBootstrapper _bootstrapper;

        public DatabaseBootstrap(DatabaseConfig config, IMigrationRunner runner)
        {
            if (config.DatabaseType == SupportedDatabases.SQLServer)
            {
                this._bootstrapper = new SqlServerBootstrapper(config, runner);
            }
            else if (config.DatabaseType == SupportedDatabases.SQLite)
            {
                this._bootstrapper = new SqliteBootstrapper(config, runner);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public IBootstrapper Bootstrapper
        {
            get
            {
                return this._bootstrapper;
            }
        }

        public void Setup()
        {
            try
            {
                _bootstrapper.RaiseUp();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while setting up database", ex);
            }
        }
    }
}