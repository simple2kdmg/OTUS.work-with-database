using Microsoft.Extensions.DependencyInjection;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Models;

namespace OTUS.work_with_database.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}