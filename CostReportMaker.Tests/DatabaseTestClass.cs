using CostReportMaker.Database;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CostReportMaker.Tests
{
    public class DatabaseTestCommon
    {
        protected static ServiceProvider? serviceProvider;

        [TestCleanup]
        public void TestInstanceCleanup()
        {
            Assert.IsNotNull(serviceProvider);

            using var scope = serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateDown(0);
        }
    }

    public class DatabaseTestWithoutSetup : DatabaseTestCommon
    {
        [TestInitialize]
        public void TestInstanceInitialize()
        {
            serviceProvider = Utils.CreateServiceProvider();
        }
    }

    public class DatabaseTestWithSetup : DatabaseTestCommon
    {
        [TestInitialize]
        public void TestInstanceInitialize()
        {
            serviceProvider = Utils.CreateServiceProvider();

            // recreate
            using var scope = serviceProvider.CreateScope();
            var bootstrap = new DatabaseBootstrap(
                Utils.GetTestDatabaseConfig(),
                scope.ServiceProvider.GetRequiredService<IMigrationRunner>()
            );
            bootstrap.Setup();
        }
    }
}