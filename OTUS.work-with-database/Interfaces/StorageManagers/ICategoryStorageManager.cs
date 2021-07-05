using System.Collections.Generic;
using System.Threading.Tasks;
using OTUS.work_with_database.Models;

namespace OTUS.work_with_database.Interfaces.StorageManagers
{
    public interface ICategoryStorageManager
    {
        public Task<IReadOnlyList<Category>> Get();
        public Task<Category> Get(long id);
        Task<int> CreateAsync(Category book);
        Task<int> UpdateAsync(Category book);
        Task<int> DeleteAsync(long id);
    }
}