using System;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using OTUS.work_with_database.Configurations;
using OTUS.work_with_database.Models;

namespace OTUS.work_with_database
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connectionString = "Host=localhost;port=5432;Database=postgres;Username=otus;Password=otus;";
            var serviceProvider = ConfigureServices(connectionString);

            // not sure if we need to create Scope here -_-
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
                var controller = new ConsoleController(new UnitOfWork(connectionString));
                await controller.RunAsync();
            }
        }

        /// <summary>
        /// Adding services using .NET Dependency Injection mechanism
        /// </summary>
        private static IServiceProvider ConfigureServices(string connectionString)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureFluentMigrator(connectionString);
            serviceCollection.ConfigureRepositories();
            serviceCollection.ConfigureStorageManagers();
            return serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Create necessary tables , if they are not exist yet (using Fluent migrator library)
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
