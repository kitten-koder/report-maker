using CostReportMaker.Database.Mapping;
using Dapper.FluentMap;
using FluentMigrator.Runner;
using Microsoft.Data.Sqlite;
using System;

namespace CostReportMaker.Database.Dialects
{
    public class SqliteBootstrapper : BootstrapperBase, IBootstrapper
    {
        public SqliteBootstrapper(DatabaseConfig config, IMigrationRunner runner) : base(config, runner)
        {
        }

        public void RaiseUp()
        {
            try
            {
                MapEntities();
            }
            catch (Exception ex)
            {
                // do nothing, maybe need to log something here
            }

            using (var _connection = new SqliteConnection(_databaseConfig.ConnectionString))
            {
                // validate the connection to make sure that is SqliteConnection
                if (_connection.GetType() != typeof(SqliteConnection))
                {
                    throw new TypeAccessException("The connection is not a SqliteConnection");
                }

                _connection.Open();

                // configure runner

                _runner.MigrateUp();
            }
        }

        private static void MapEntities()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMap());
            });
        }
    }
}