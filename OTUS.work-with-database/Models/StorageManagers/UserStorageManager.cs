using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Interfaces.StorageManagers;

namespace OTUS.work_with_database.Models.StorageManagers
{
    public class UserStorageManager : IUserStorageManager
    {
        private readonly IUnitOfWork _uow;

        public UserStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<User>> Get()
            => await _uow.UsersRepository.GetAsync();

        public async Task<User> Get(long id)
            => await _uow.UsersRepository.GetAsync(id);

        public async Task<int> CreateAsync(User user)
        {
            var result = await _uow.UsersRepository.CreateAsync(user);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(User user)
        {
            var result = await _uow.UsersRepository.UpdateAsync(user);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.UsersRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> FillTableWithInitialValues()
        {
            var users = await Get();
            if (users.Any())
                return 0;

            var result = await _uow.UsersRepository.FillWithInitialValues();
            _uow.Commit();
            return result;
        }
    }
}