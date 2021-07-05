using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Interfaces.StorageManagers;

namespace OTUS.work_with_database.Models.StorageManagers
{
    public class AdvertisementStorageManager : IAdvertisementStorageManager
    {
        private readonly IUnitOfWork _uow;

        public AdvertisementStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<Advertisement>> Get()
            => await _uow.AdvertisementsRepository.GetAsync();

        public async Task<Advertisement> Get(long id)
            => await _uow.AdvertisementsRepository.GetAsync(id);

        public async Task<int> CreateAsync(Advertisement ad)
        {
            var result = await _uow.AdvertisementsRepository.CreateAsync(ad);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Advertisement ad)
        {
            var result = await _uow.AdvertisementsRepository.UpdateAsync(ad);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.AdvertisementsRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> FillTableWithInitialValues()
        {
            var advertisements = await Get();
            if (advertisements.Any())
                return 0;

            var result = await _uow.AdvertisementsRepository.FillWithInitialValues();
            _uow.Commit();
            return result;
        }
    }
}