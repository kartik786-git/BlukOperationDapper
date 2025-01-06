using BlukOperationDapper.Data;
using BlukOperationDapper.Model;
using Dapper;

namespace BlukOperationDapper.Services
{
    public class PersonService : IPersonService
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public PersonService(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task BulkDeleteAsync(IEnumerable<int> ids)
        {
            using (var conneciton = _dbConnectionProvider.CreateConnection())
            {
                var sql = "DELETE FROM Person WHERE Id IN @Ids";
                await conneciton.ExecuteAsync(sql, new { Ids = ids });
            }
        }

        public async Task BulkInsertAsync(IEnumerable<Person> people)
        {
            using (var connection = _dbConnectionProvider.CreateConnection())
            {
                string query = @"INSERT INTO Person (FirstName, LastName, Age) 
                        VALUES (@FirstName, @LastName, @Age)";
                await connection.ExecuteAsync(query, people);
            }
        }

        public async Task BulkUpdateAsync(IEnumerable<Person> people)
        {
            using (var conneciton = _dbConnectionProvider.CreateConnection())
            {
                string query = @"
                UPDATE Person
                SET FirstName = @FirstName, LastName = @LastName, Age = @Age
                WHERE Id = @Id";

                await conneciton.ExecuteAsync(query, people);
            }
        }
    }
}
