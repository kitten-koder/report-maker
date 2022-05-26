using CostReportMaker.Database;
using CostReportMaker.Properties;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace CostReportMaker
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            var databaseBootstrap = serviceProvider.GetService<IDatabaseBootstrap>();
            databaseBootstrap.Setup();

            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        /// <summary>
        /// Setup Dependencies Injection
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(new DatabaseConfig
            {
                ConnectionString = Settings.Default.DatabaseName,
                DatabaseType = (SupportedDatabases)Enum.ToObject(typeof(SupportedDatabases), Settings.Default.DatabaseType)
            }); ;

            services
                .AddLogging(
                    configure => configure
                    .AddConsole()
                    .AddFluentMigratorConsole()
                )
                .AddFluentMigratorCore()
                .AddFluentMigratorRunnerConfigurator()
                .AddScoped<IDatabaseBootstrap, DatabaseBootstrap>()
                .AddApplicationRepositories();

            services.AddScoped<MainForm>();
        }
    }
}