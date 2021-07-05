using Microsoft.Extensions.DependencyInjection;
using OTUS.work_with_database.Interfaces.StorageManagers;
using OTUS.work_with_database.Models.StorageManagers;

namespace OTUS.work_with_database.Configurations
{
    public static class StorageManagersConfiguration
    {
        public static void ConfigureStorageManagers(this IServiceCollection services)
        {
            services.AddTransient<IUserStorageManager, UserStorageManager>();
            services.AddTransient<IAdvertisementStorageManager, AdvertisementStorageManager>();
            services.AddTransient<ICategoryStorageManager, CategoryStorageManager>();
        }
    }
}