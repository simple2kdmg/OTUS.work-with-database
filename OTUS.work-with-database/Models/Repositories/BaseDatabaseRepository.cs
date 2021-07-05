using System.Data;

namespace OTUS.work_with_database.Models.Repositories
{
    public abstract class BaseDatabaseRepository
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction?.Connection;

        protected BaseDatabaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}