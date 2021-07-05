using System.Collections.Generic;
using System.Threading.Tasks;
using OTUS.work_with_database.Models;

namespace OTUS.work_with_database.Interfaces.StorageManagers
{
    public interface IAdvertisementStorageManager
    {
        public Task<IReadOnlyList<Advertisement>> Get();
        public Task<Advertisement> Get(long id);
        Task<int> CreateAsync(Advertisement book);
        Task<int> UpdateAsync(Advertisement ad);
        Task<int> DeleteAsync(long id);
    }
}