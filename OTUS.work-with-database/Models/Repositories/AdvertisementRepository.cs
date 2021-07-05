using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OTUS.work_with_database.Interfaces.Repositories;

namespace OTUS.work_with_database.Models.Repositories
{
    public class AdvertisementRepository : BaseDatabaseRepository, IAdvertisementRepository
    { 
        public AdvertisementRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<Advertisement> GetAsync(long id)
        {
            var sql = @"SELECT * FROM advertisements WHERE id = @id";
            var result = await Connection.QueryAsync<Advertisement>(sql, new { id });
            return result.Single();
        }

        public async Task<IReadOnlyList<Advertisement>> GetAsync()
        {
            var sql = @"SELECT * FROM advertisements";
            var result = await Connection.QueryAsync<Advertisement>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Advertisement entity)
        {
            if (entity == null) return 0;

            var sql = @"
                INSERT INTO advertisements (name, description, price, user_id, category_id)
                VALUES (@Name, @Description, @Price, @UserId, @CategoryId)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(Advertisement entity)
        {
            if (entity == null) return 0;

            var sql = @"
                UPDATE advertisements
                SET name = @Name,
                    description = @Description,
                    price = @Price,
                    user_id = @UserId,
                    category_id = @CategoryId
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"
                DELETE FROM advertisements
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> FillWithInitialValues()
        {
            var sql = @"
                INSERT INTO advertisements (name, description, price, user_id, category_id)
                VALUES
                    ('Toyota', 'Good one', 350000, 1, 3),
                    ('Mazda', 'Another good one', 320000, 2, 3),
                    ('Refrigerator LG', 'Been used for 2 years only', 16000, 1, 1),
                    ('Harry Potter and the Chamber of secrets', '-', 400, 3, 4),
                    ('Table lamp', 'Red and pretty', 1200, 4, 2)
            ";
            return await Connection.ExecuteAsync(sql, Transaction);
        }
    }
}