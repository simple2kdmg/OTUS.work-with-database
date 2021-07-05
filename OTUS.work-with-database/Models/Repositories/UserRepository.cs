using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OTUS.work_with_database.Interfaces.Repositories;

namespace OTUS.work_with_database.Models.Repositories
{
    public class UserRepository : BaseDatabaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
 
        }

        public async Task<User> GetAsync(long id)
        {
            var sql = @"SELECT * FROM users WHERE id = @id";
            var result = await Connection.QueryAsync<User>(sql, new { id });
            return result.Single();
        }

        public async Task<IReadOnlyList<User>> GetAsync()
        {
            var sql = @"SELECT * FROM users";
            var result = await Connection.QueryAsync<User>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(User entity)
        {
            if (entity == null) return 0;

            var sql = @"
                INSERT INTO users (name, surname, age, email)
                VALUES (@Name, @Surname, @Age, @Email)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(User entity)
        {
            if (entity == null) return 0;

            var sql = @"
                UPDATE users
                SET name = @Name,
                    surname = @Surname,
                    age = @Age,
                    email = @Email
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"
                DELETE FROM users
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> FillWithInitialValues()
        {
            var sql = @"
                INSERT INTO users (name, surname, age, email)
                VALUES
                    ('Andrey', 'Semenov', 26, 'asemenov@mail.ru'),
                    ('Evgeniy', 'Petrov', 33, 'epetrov@gmail.com'),
                    ('Anton', 'Dolgov', 41, 'adolgov@mail.ru'),
                    ('Daria', 'Sinitsina', 23, 'dsinitsina@gmail.com')
            ";
            return await Connection.ExecuteAsync(sql, Transaction);
        }
    }
}