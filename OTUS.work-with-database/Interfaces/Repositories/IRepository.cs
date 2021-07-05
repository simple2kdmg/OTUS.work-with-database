using System.Collections.Generic;
using System.Threading.Tasks;

namespace OTUS.work_with_database.Interfaces.Repositories
{
    public interface IRepository<T, in TArg> where T : class
    {
        Task<T> GetAsync(TArg id);
        Task<IReadOnlyList<T>> GetAsync();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(TArg id);
        Task<int> FillWithInitialValues();
    }
}