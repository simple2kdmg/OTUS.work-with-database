using System.Collections.Generic;
using System.Threading.Tasks;
using OTUS.work_with_database.Models;

namespace OTUS.work_with_database.Interfaces.StorageManagers
{
    public interface IUserStorageManager
    {
        public Task<IReadOnlyList<User>> Get();
        public Task<User> Get(long id);
        Task<int> CreateAsync(User book);
        Task<int> UpdateAsync(User book);
        Task<int> DeleteAsync(long id);
    }
}