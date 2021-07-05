using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OTUS.work_with_database.Interfaces.Repositories;

namespace OTUS.work_with_database.Models.Repositories
{
    public class CategoryRepository : BaseDatabaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<Category> GetAsync(long id)
        {
            var sql = @"SELECT * FROM categories WHERE id = @id";
            var result = await Connection.QueryAsync<Category>(sql, new { id });
            return result.Single();
        }

        public async Task<IReadOnlyList<Category>> GetAsync()
        {
            var sql = @"SELECT * FROM categories";
            var result = await Connection.QueryAsync<Category>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Category entity)
        {
            if (entity == null) return 0;

            var sql = @"
                INSERT INTO categories (name, description)
                VALUES (@Name, @Description)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(Category entity)
        {
            if (entity == null) return 0;

            var sql = @"
                UPDATE categories
                SET name = @Name,
                    description = @Description
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"
                DELETE FROM categories
                WHERE id = @Id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> FillWithInitialValues()
        {
            var sql = @"
                INSERT INTO categories (name, description)
                VALUES
                    ('Appliances', 'Refrigerators, washing machines, microwave ovens, etc.'),
                    ('Home', 'Household items'),
                    ('Cars', 'Cars for sale'),
                    ('Books', 'Used books')
            ";
            return await Connection.ExecuteAsync(sql, Transaction);
        }
    }
}