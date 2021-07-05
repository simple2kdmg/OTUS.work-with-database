using System;
using System.Data;
using Npgsql;
using OTUS.work_with_database.Interfaces;
using OTUS.work_with_database.Interfaces.Repositories;
using OTUS.work_with_database.Models.Repositories;

namespace OTUS.work_with_database.Models
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUserRepository _userRepository;
        private IAdvertisementRepository _advertisementRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IUserRepository UsersRepository =>
            _userRepository ??= new UserRepository(_transaction);

        public IAdvertisementRepository AdvertisementsRepository =>
            _advertisementRepository ??= new AdvertisementRepository(_transaction);

        public ICategoryRepository CategoriesRepository =>
            _categoryRepository ??= new CategoryRepository(_transaction);

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _userRepository = null;
            _advertisementRepository = null;
            _categoryRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _transaction?.Dispose();
            _transaction = null;
            _connection?.Dispose();
            _connection = null;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}