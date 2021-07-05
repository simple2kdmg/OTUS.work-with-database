using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using OTUS.work_with_database.Migrations;

namespace OTUS.work_with_database.Configurations
{
    public static class FluentMigratorConfiguration
    {
        /// <summary>
        /// Configuring fluent migrator, using example from official documentation
        /// </summary>
        public static void ConfigureFluentMigrator(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore().ConfigureRunner(config =>
                config.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AddAdvertisementTable).Assembly).For.Migrations()
                );
        }
    }
}