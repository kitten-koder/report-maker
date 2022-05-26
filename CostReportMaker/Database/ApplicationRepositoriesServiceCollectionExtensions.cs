using CostReportMaker.Database.Repository;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationRepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services

                // Add UnitOfWork
                .AddScoped<IUnitOfWork, UnitOfWork>()

                // Add UserProvider
                .AddScoped<IUserProvider, UserRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}