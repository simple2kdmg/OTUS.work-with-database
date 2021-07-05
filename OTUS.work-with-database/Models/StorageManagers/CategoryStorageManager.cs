using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Interfaces.StorageManagers;

namespace OTUS.work_with_database.Models.StorageManagers
{
    public class CategoryStorageManager : ICategoryStorageManager
    {
        private readonly IUnitOfWork _uow;

        public CategoryStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<Category>> Get()
            => await _uow.CategoriesRepository.GetAsync();

        public async Task<Category> Get(long id)
            => await _uow.CategoriesRepository.GetAsync(id);

        public async Task<int> CreateAsync(Category ctg)
        {
            var result = await _uow.CategoriesRepository.CreateAsync(ctg);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Category ctg)
        {
            var result = await _uow.CategoriesRepository.UpdateAsync(ctg);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.CategoriesRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> FillTableWithInitialValues()
        {
            var categories = await Get();
            if (categories.Any())
                return 0;

            var result = await _uow.CategoriesRepository.FillWithInitialValues();
            _uow.Commit();
            return result;
        }
    }
}