using FluentMigrator.Runner;
using System;

namespace CostReportMaker.Database.Dialects
{
    public class SqlServerBootstrapper : BootstrapperBase, IBootstrapper
    {
        public SqlServerBootstrapper(DatabaseConfig config, IMigrationRunner runner) : base(config, runner)
        { }

        public void RaiseUp()
        {
            throw new NotImplementedException();
        }
    }
}