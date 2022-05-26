using CostReportMaker.Database.Dialects;
using CostReportMaker.Tests;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CostReportMaker.Database.Database.Tests
{
    [TestClass()]
    public class DatabaseBootstrapTests : DatabaseTestWithoutSetup
    {
        [TestMethod()]
        public void SetupTest()
        {
            using var scope = serviceProvider.CreateScope();

            var bootstrap = new DatabaseBootstrap(
                Utils.GetTestDatabaseConfig(),
                scope.ServiceProvider.GetRequiredService<IMigrationRunner>()
            );
            bootstrap.Setup();

            Assert.AreEqual(bootstrap.Bootstrapper.GetType(), typeof(SqliteBootstrapper));
        }
    }
}