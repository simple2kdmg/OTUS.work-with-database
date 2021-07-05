using System;
using OTUS.work_with_database.Interfaces.Repositories;

namespace OTUS.work_with_database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UsersRepository { get; }
        IAdvertisementRepository AdvertisementsRepository { get; }
        ICategoryRepository CategoriesRepository { get; }

        void Commit();
    }
}